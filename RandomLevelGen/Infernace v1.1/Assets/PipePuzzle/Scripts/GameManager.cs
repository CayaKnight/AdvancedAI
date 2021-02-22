using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool GenerateRandom;
	public GameObject[] PiecesPrefabs;
	public GameObject canvas;

	[System.Serializable]
	public class Puzzle
	{

		public int winValue;
		public int curValue;

		public int width;
		public int height;
		public Pieces[,] Pieces;

	}


	public Puzzle puzzle;


	// Use this for initialization
	void Start()
	{

		canvas.SetActive(false);

		if (GenerateRandom)
		{

			if (puzzle.width == 0 || puzzle.height == 0)
			{

				Debug.LogError("Please set the dimensions");
				Debug.Break();

			}

			GeneratePuzzle();








		}
		else
		{

			Vector2 dimensions = CheckDimensions();

			puzzle.width = (int)dimensions.x;
			puzzle.height = (int)dimensions.y;


			puzzle.Pieces = new Pieces[puzzle.width, puzzle.height];



			foreach (var Pieces in GameObject.FindGameObjectsWithTag("Pieces"))
			{

				puzzle.Pieces[(int)Pieces.transform.position.x, (int)Pieces.transform.position.y] = Pieces.GetComponent<Pieces>();

			}
		}


		foreach (var item in puzzle.Pieces)
		{

			Debug.Log(item.gameObject.name);
		}


		puzzle.winValue = GetWinValue();

		Shuffle();

		puzzle.curValue = Sweep();


	}


	void GeneratePuzzle()
	{
		puzzle.Pieces = new Pieces[puzzle.width, puzzle.height];

		int[] auxValues = { 0, 0, 0, 0 };


		for (int h = 0; h < puzzle.height; h++)
		{
			for (int w = 0; w < puzzle.width; w++)
			{



				//width restrictions
				if (w == 0)
					auxValues[3] = 0;
				else
					auxValues[3] = puzzle.Pieces[w - 1, h].values[1];

				if (w == puzzle.width - 1)
					auxValues[1] = 0;
				else
					auxValues[1] = Random.Range(0, 2);


				//heigth resctrictions

				if (h == 0)
					auxValues[2] = 0;
				else
					auxValues[2] = puzzle.Pieces[w, h - 1].values[0];

				if (h == puzzle.height - 1)
					auxValues[0] = 0;
				else
					auxValues[0] = Random.Range(0, 2);


				//tells us Pieces type
				int valueSum = auxValues[0] + auxValues[1] + auxValues[2] + auxValues[3];


				if (valueSum == 2 && auxValues[0] != auxValues[2])
					valueSum = 5;


				GameObject go = (GameObject)Instantiate(PiecesPrefabs[valueSum], new Vector3(w, h, 0), Quaternion.identity);



				while (go.GetComponent<Pieces>().values[0] != auxValues[0] ||
					  go.GetComponent<Pieces>().values[1] != auxValues[1] ||
					  go.GetComponent<Pieces>().values[2] != auxValues[2] ||
					  go.GetComponent<Pieces>().values[3] != auxValues[3])

				{
					go.GetComponent<Pieces>().RotatePiece();

				}

				puzzle.Pieces[w, h] = go.GetComponent<Pieces>();



			}
		}



	}


	public int Sweep()
	{
		int value = 0;

		for (int h = 0; h < puzzle.height; h++)
		{
			for (int w = 0; w < puzzle.width; w++)
			{


				//compares top
				if (h != puzzle.height - 1)
					if (puzzle.Pieces[w, h].values[0] == 1 && puzzle.Pieces[w, h + 1].values[2] == 1)
						value++;


				//compare right
				if (w != puzzle.width - 1)
					if (puzzle.Pieces[w, h].values[1] == 1 && puzzle.Pieces[w + 1, h].values[3] == 1)
						value++;


			}
		}

		return value;

	}


	public void Win()
	{

		canvas.SetActive(true);
	}

	public int QuickSweep(int w, int h)
	{
		int value = 0;

		//compares top
		if (h != puzzle.height - 1)
			if (puzzle.Pieces[w, h].values[0] == 1 && puzzle.Pieces[w, h + 1].values[2] == 1)
				value++;


		//compare right
		if (w != puzzle.width - 1)
			if (puzzle.Pieces[w, h].values[1] == 1 && puzzle.Pieces[w + 1, h].values[3] == 1)
				value++;


		//compare left
		if (w != 0)
			if (puzzle.Pieces[w, h].values[3] == 1 && puzzle.Pieces[w - 1, h].values[1] == 1)
				value++;

		//compare bottom
		if (h != 0)
			if (puzzle.Pieces[w, h].values[2] == 1 && puzzle.Pieces[w, h - 1].values[0] == 1)
				value++;


		return value;

	}

	int GetWinValue()
	{

		int winValue = 0;
		foreach (var Pieces in puzzle.Pieces)
		{


			foreach (var j in Pieces.values)
			{
				winValue += j;
			}


		}

		winValue /= 2;

		return winValue;



	}

	void Shuffle()
	{
		foreach (var Pieces in puzzle.Pieces)
		{

			int k = Random.Range(0, 4);

			for (int i = 0; i < k; i++)
			{
				Pieces.RotatePiece();
			}


		}
	}


	Vector2 CheckDimensions()
	{
		Vector2 aux = Vector2.zero;

		GameObject[] Pieces = GameObject.FindGameObjectsWithTag("Pieces");

		foreach (var p in Pieces)
		{
			if (p.transform.position.x > aux.x)
				aux.x = p.transform.position.x;

			if (p.transform.position.y > aux.y)
				aux.y = p.transform.position.y;
		}

		aux.x++;
		aux.y++;

		return aux;
	}

	// Update is called once per frame
	void Update()
	{

	}

}
