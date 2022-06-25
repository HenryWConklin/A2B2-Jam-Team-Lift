using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSequenceUser : MonoBehaviour
{
    public int widthPitch = 35;
    public int heightPitch = 38;
    private MidiSequencer sequencer;
    // Start is called before the first frame update
    void Start()
    {
        sequencer = GetComponent<MidiSequencer>();
    }

    // Update is called once per frame
    void Update()
    {
        List<MidiSequencer.Note> notes = sequencer.GetActiveNotes();
        float xScale = 1.0f;
        float yScale = 1.0f;
        foreach (var n in notes) {
            if (n.Pitch == widthPitch) {
                xScale = 1.5f;
            }
            else if (n.Pitch == heightPitch) {
                yScale = 1.5f;
            }
        }
        transform.localScale = new Vector3(xScale, yScale, 1.0f);
    }
}
