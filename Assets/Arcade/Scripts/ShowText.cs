using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Clase que muestra un texto p�rrafo por p�rrafo
//Presionar cualquier tecla de ataque progresa el texto y cambia la escena al llegar al final
//utilizar 1 solo archivo de texto por escena
namespace Arcade
{
    public class ShowText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMesh;

        //Tiempo de espera entre cada letra mostrada
        //Definir en cada escena
        [SerializeField]
        private float _textDelay;

        //Archivo del texto que ser� reproducido en escena
        [SerializeField]
        private TextAsset _textFile;

        //Texto bruto. Puede definirse por inspector en caso que no se tenga archivo
        [SerializeField]
        [TextArea(7, 10)]
        private string _fullText;

        //C�racter que define la separaci�n de p�rrafos
        //Definir en cada escena por inspector
        //Se recomienda usar >
        [SerializeField]
        private string _lineSeparator = "";

        private string[] _lines;
        private int _lineIndex;
        private bool _showingTextLine;

        //M�todo que divide el texto en p�rrafos
        void ParseText()
        {
            //En caso que se tenga asignado un archivo de texto, se utiliza ese en vez de lo escrito en el inspector
            if (_textFile != null)
                _fullText = _textFile.text;
            //El texto se separa en "p�rrafos" separados por el caracter separador
            //El caracter separador NO se mostrar� en pantalla
            _lines = _fullText.Split(_lineSeparator, System.StringSplitOptions.RemoveEmptyEntries);
            _lineIndex = 0;
        }

        //Corrutina que muestra 1 p�rrafo de texto caracter por caracter hasta llegar al fin del p�rrafo
        //Modificar _textDelay para cambiar velocidad
        IEnumerator DisplayTextLine()
        {
            if (_lineIndex < _lines.Length)
            {
                _showingTextLine = true;
                int charIndex = 0;
                _textMesh.text = _lines[_lineIndex];

                while (charIndex < _lines[_lineIndex].Length)
                {
                    charIndex++;
                    _textMesh.maxVisibleCharacters = charIndex;
                    yield return new WaitForSeconds(_textDelay);
                }
            }
            _showingTextLine = false;
        }

        //M�todo que muestra inmediatamente el p�rrafo, sin la animaci�n de la corrutina
        void ShowFullLine()
        {
            if (_showingTextLine)
            {
                StopAllCoroutines();
                _textMesh.maxVisibleCharacters = _lines[_lineIndex].Length;
                _showingTextLine = false;
            }
        }


        //M�todo que muestra el siguiente p�rrafo si ya se termin� de mostrar uno
        //En caso contrario, llama ShowFullLine(), salta la animaci�n del texto y muestra el p�rrafo inmediatamente
        //En caso que sea la l�nea final, cambia de escena
        void ShowNextLine()
        {
            if (_lineIndex >= _lines.Length -1)
            {
                SkipToNextScene();
            }
            else if (!_showingTextLine)
            {
                _lineIndex++;
                StartCoroutine(DisplayTextLine());
            }
            else
            {
                ShowFullLine();
            }
        }

        //M�todo que cambia a la siguiente escena en caso que se haya terminado de mostrar el�p�rrafo actual
        void SkipToNextScene()
        {
            if (!_showingTextLine)
            {
                GameSceneManager.NextLevel();
            }
            else
            {
                ShowFullLine();
            }
        }

        private void Awake()
        {
            ParseText();
        }
        private void Start()
        {
            if(_fullText.Length != 0)
                StartCoroutine(DisplayTextLine());
            else
                GameSceneManager.NextLevel();
        }


        private void Update()
        {
            if (Input.GetButtonDown("P1_Start") || Input.GetButtonDown("P2_Start") || Input.GetButtonDown("P1_Fire1") ||  Input.GetButtonDown("P2_Fire1") || Input.GetButtonDown("P1_Fire2") || Input.GetButtonDown("P2_Fire2"))
            {
                ShowNextLine();
            }
        }
    }
}