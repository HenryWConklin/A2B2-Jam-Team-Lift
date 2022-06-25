using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnerController : MonoBehaviour
{
    public int spawnNote = 35;
    public int stepNote = 38;
    private MidiSequencer sequencer;
    private EnemySpawner spawner;
    private List<TetrominoEnemy> enemies;
    // Start is called before the first frame update
    void Start()
    {
        sequencer = GetComponent<MidiSequencer>();
        spawner = GetComponent<EnemySpawner>();
        enemies = new List<TetrominoEnemy>();
    }

    void FixedUpdate()
    {
        // Remove destroyed enemies
        enemies.RemoveAll(x => x == null);

        foreach (var note in sequencer.GetJustActiveNotes()) {
            if (note.Pitch == spawnNote) {
                TetrominoEnemy enemy =spawner.Spawn().GetComponent<TetrominoEnemy>();
                if (enemy != null) {
                enemies.Add(enemy);
                }
                else {
                    print("Enemy missing TetrominoEnemy script");
                }
            }
            else if (note.Pitch == stepNote) {
                enemies.ForEach(x => x.Step());
            }
        }
    }
}
