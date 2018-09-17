using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField] Transform _levelGeneratorTransform;
    [SerializeField] Transform _levelTilesParentTransform;


    private int _levelLength;
	private LevelPiece[] _levelPieces = new LevelPiece[1];
    [SerializeField] private GameObject[] _tilePrefabs;




    private void Start()
	{
        _levelPieces = GenerateLevelPieces();
        GenerateSectors();
    }
	private void GenerateSectors(){

        for (int i = 0; i < _levelPieces.Length; i++){


            for (int x = 0; x < _levelPieces[i].Data.GetLength(0); x++)
            {
                for (int y = 0; y <_levelPieces[i].Data.GetLength(1); y++)
                {
					if(_levelPieces[i].Data[x,y] != 0)
						Instantiate(_tilePrefabs[_levelPieces[i].Data[x,y]], _levelGeneratorTransform.position, Quaternion.identity,_levelTilesParentTransform);

                    _levelGeneratorTransform.position = new Vector3(_levelGeneratorTransform.position.x - 1, .125f, _levelGeneratorTransform.position.z);
                }
                _levelGeneratorTransform.position = new Vector3(_levelGeneratorTransform.position.x + 6, .125f, _levelGeneratorTransform.position.z +1);
            }
		}
    }

    // I wanted to make a separate application to make your own but I didn't bother.
    private LevelPiece[] GenerateLevelPieces(){

        LevelPiece[] newLevelPieces = new LevelPiece[4];

        newLevelPieces[0] = new LevelPiece("FallingTiles", 10, new int[10, 6] {
            { 3, 2, 2, 0, 0, 3 },
            { 0, 0, 0, 2, 0, 0 },
            { 0, 2, 2, 0, 3, 0 },
            { 2, 0, 3, 0, 0, 2 },
            { 3, 0, 2, 2, 0, 0 },
            { 0, 0, 2, 0, 3, 2 },
            { 2, 3, 2, 3, 2, 2 },
            { 1, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }
		});

        newLevelPieces[1] = new LevelPiece("SpikeTrap", 10, new int[10, 6] {
         
            { 0, 1, 1, 0, 0, 1 },
            { 0, 0, 0, 1, 3, 0 },
            { 1, 0, 0, 0, 0, 0 },
            { 0, 3, 1, 1, 0, 3 },
            { 0, 0, 0, 1, 0, 0 },
            { 1, 1, 0, 0, 1, 0 },
            { 0, 0, 1, 0, 0, 0 },
            { 1, 0, 0, 3, 1, 1 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }
		});

        newLevelPieces[2] = new LevelPiece("3", 10, new int[10, 6] {
            { 0, 2, 2, 1, 2, 1 },
            { 3, 0, 0, 0, 3, 3 },
            { 0, 3, 0, 1, 0, 1 },
            { 0, 2, 0, 0, 0, 3 },
            { 2, 0, 0, 0, 0, 1 },
            { 0, 2, 2, 1, 0, 1 },
            { 1, 0, 0, 0, 2, 1 },
            { 0, 3, 3, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }
        });

        newLevelPieces[3] = new LevelPiece("4", 10, new int[10, 6] {
            { 2, 0, 2, 0, 2, 0 },
            { 0, 2, 0, 2, 0, 2 },
            { 0, 0, 2, 0, 2, 0 },
            { 0, 2, 0, 2, 0, 1 },
            { 2, 0, 2, 0, 1, 0 },
            { 0, 1, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 2, 1 },
            { 0, 2, 2, 2, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }
        });

        return newLevelPieces;

    }
}
