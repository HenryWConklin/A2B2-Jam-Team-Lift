using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MidiSequencer : MonoBehaviour
{
    public TextAsset MidiCsv;
    public float TempoBpm = 120;
    public bool Loop;
    public bool Playing;

    private float SequenceTime;
    private int NextEvent;
    private List<Note> NoteEvents;
    // Pitch -> active note 
    private Dictionary<int, Note> ActiveNotes;
    // Pitch -> notes that just became active since the last frame
    private Dictionary<int, Note> JustActiveNotes;

    // Start is called before the first frame update
    void Start()
    {
        NextEvent = 0;
        SequenceTime = 0;
        ActiveNotes = new Dictionary<int, Note>();
        JustActiveNotes = new Dictionary<int, Note>();
        NoteEvents = ParseMidiCsv(MidiCsv.text, TempoBpm);
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing)
        {
            JustActiveNotes.Clear();
            SequenceTime += Time.deltaTime;
            while (NextEvent < NoteEvents.Count && NoteEvents[NextEvent].Timestamp <= SequenceTime)
            {
                var note = NoteEvents[NextEvent];
                if (note.Velocity == 0)
                {
                    ActiveNotes.Remove(note.Pitch);
                }
                else
                {
                    ActiveNotes.TryAdd(note.Pitch, note);
                    JustActiveNotes.TryAdd(note.Pitch, note);
                }
                NextEvent += 1;
            }
            if (Loop && NextEvent >= NoteEvents.Count)
            {
                SequenceTime %= Duration();
                NextEvent = 0;
                ActiveNotes.Clear();
            }
        }
    }
    /// Duration of the track in seconds
    public float Duration()
    {
        return NoteEvents[NoteEvents.Count - 1].Timestamp;
    }

    /// Start the track.
    public void Play()
    {
        Playing = true;
    }

    /// Pause the track and keep the current position.
    public void Pause()
    {
        Playing = false;
    }
    /// Stop the track and reset to the beginning.
    public void Stop()
    {
        Playing = false;
        SequenceTime = 0.0f;
        NextEvent = 0;
        ActiveNotes.Clear();
        JustActiveNotes.Clear();
    }
    public List<Note> GetActiveNotes()
    {
        return ActiveNotes.Values.ToList();
    }

    public List<Note> GetJustActiveNotes()
    {
        return JustActiveNotes.Values.ToList();
    }

    public struct Note
    {
        public Note(int track, int pitch, int velocity, float timestamp)
        {
            Track = track;
            Pitch = pitch;
            Velocity = velocity;
            Timestamp = timestamp;
        }
        public int Track { get; }
        public int Pitch { get; }
        public int Velocity { get; }
        public float Timestamp { get; }
    }

    private struct MidiEvent
    {
        public MidiEvent(string line)
        {
            string[] cols = line.Split(',');
            TrackNum = int.Parse(cols[0].Trim());
            Timestamp = int.Parse(cols[1].Trim());
            EventName = cols[2].Trim();
            Args = cols.Skip(3).SelectMany(x =>
            {
                int val;
                if (int.TryParse(x.Trim(), out val))
                {
                    return new int[] { val };
                }
                else
                {
                    return new int[] { };
                }
            }).ToArray();
        }
        public int TrackNum { get; }
        public int Timestamp { get; }
        public string EventName { get; }
        public int[] Args { get; }
    }


    private static List<Note> ParseMidiCsv(string text, float tempo)
    {
        List<MidiEvent> events = text.Split('\n').Select(x => x.Trim()).Where(x => x.Count() > 0).Select(line => new MidiEvent(line)).ToList();
        foreach (var e in events)
        {
            print(e.EventName);
        }
        int stepsPerQuarter = events.Find(e => e.EventName == "Header").Args[2];
        float stepsPerSec = stepsPerQuarter * tempo / 60.0f;
        List<Note> result = new List<Note>();

        for (int i = 0; i < events.Count; i++)
        {
            var e = events[i];
            if (e.EventName == "Note_on_c" || e.EventName == "Note_off_c")
            {
                int pitch = e.Args[1];
                int velocity = e.Args[2];
                // Note off recorded as a 0 velocity note
                if (e.EventName == "Note_off_c") velocity = 0;
                // Note on with a velocity of 0 counts as a Note off
                result.Add(new Note(e.TrackNum, pitch, velocity, e.Timestamp / stepsPerSec));
            }
            else if (e.EventName == "End_track")
            {
                // Add a note off to pad to the end of the track
                result.Add(new Note(e.TrackNum, 0, 0, e.Timestamp / stepsPerSec));
            }
        }

        result.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp));
        return result;
    }

}
