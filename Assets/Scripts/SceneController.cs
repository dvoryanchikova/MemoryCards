using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public const int gridRows = 2;
	public const int gridCols = 4;
	public const float offsetX = 2.25f;
	public const float offsetY = 3.0f;

	private MemoryCard _firstReveal;
	private MemoryCard _secondReveal;

	private int _score = 0;

	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;

	[SerializeField] private TextMesh scoreLabel;


	public bool canReveal{
		get{return _secondReveal == null;}
	}
		
	void Start () {	
		Vector3 starPos = originalCard.transform.position;

		int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
		numbers = ShuffleArray(numbers);

		for(int i = 0; i < gridCols; i++){
			for(int j = 0; j < gridRows; j++){
				MemoryCard card;
				if( i == 0 && j == 0){
					card = originalCard;
				} else {
					card = Instantiate (originalCard) as MemoryCard;
				}
					
				int index = j * gridCols + i;
				int id = numbers [index];
				card.SetCard (id, images [id]);


				float posX = (offsetX * i) + starPos.x;
				float posY = -(offsetY * j) + starPos.y;
				card.transform.position = new Vector3 (posX, posY, starPos.z);
			}
		}
	}

	private int[] ShuffleArray(int[] numbers){
		int[] newArray = numbers.Clone () as int[];
		for(int i = 0; i < newArray.Length; i++ ){
			int tmp = newArray [i];
			int r = Random.Range (i, newArray.Length);
			newArray [i] = newArray [r];
			newArray [r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed( MemoryCard card){
		if(_firstReveal == null){
			_firstReveal = card;
		} else {
			_secondReveal = card;
			Debug.Log ("Match? " + (_firstReveal.id == _secondReveal.id));
			StartCoroutine (CheckMatch());
		}
	}

	private IEnumerator CheckMatch(){
		if( _firstReveal.id == _secondReveal.id){
			_score++;
			scoreLabel.text = "Score: " + _score;
			Debug.Log ("Score: " + _score);
		}
		else {
			yield return new WaitForSeconds (.5f);

			_firstReveal.Unreveal ();
			_secondReveal.Unreveal ();
		}

		_firstReveal = null;
		_secondReveal = null;
	}

	public void Restart(){
		Application.LoadLevel ("UIA2d");
	}

}
   