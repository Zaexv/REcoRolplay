using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace REcoSample
{
	public partial class Formulario_Rolplay : Form
	{
		private System.Speech.Recognition.SpeechRecognitionEngine _recognizer =
		   new SpeechRecognitionEngine();
		private SpeechSynthesizer synth = new SpeechSynthesizer();


		//Variables privadas para usar en las funciones //
		private Grammar grammarColors, grammarNombres, grammarYesNo;
		private int state; //El estado del di�logo

		public Formulario_Rolplay()
		{
			//Esto pinta el formulario.
			InitializeComponent();
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			synth.Speak("Estimado ser humano. Has conseguido viajar en tiempo para salvar a la humanidad de la devastacion total.");
			//Inicializo las gram�tica y todos sus componentes

			//Inicializo la variable global de estado
			state = 0;

			//Creo las gram�ticas con las funciones para crearlas
			grammarColors = CreateGrammarColors(null);
			grammarNombres = CreateGrammarName(null);
			grammarYesNo = CreateGrammarYesNo(null);

			//No cambiar, inicializando el recognizer
			_recognizer.SetInputToDefaultAudioDevice();
			_recognizer.UnloadAllGrammars();
			// Nivel de confianza del reconocimiento 60%
			_recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 60);

			//Activo la gram�ticas creadas previamente.
			//ActivarGramatica(grammarNombres);
			//ActivarGramatica(grammarColors);
			ActivarGramatica(grammarYesNo);

			//No cambiar, inicializando el recognizer
			_recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
			//reconocimiento as�ncrono y m�ltiples veces
			_recognizer.RecognizeAsync(RecognizeMode.Multiple);
			synth.Speak("Ahora cuentame; ¿Quieres participar en nuestra lucha?");
		}

		//Activa una gram�tica para ser usada
		private void ActivarGramatica(Grammar grammar)
		{
			grammar.Enabled = true;
			_recognizer.LoadGrammar(grammar);
		}

		//Desactiva una gram�tica para ser usada
		private void DesactivarGramatica(Grammar grammar)
        {
			grammar.Enabled = false;
			_recognizer.UnloadGrammar(grammar);
        }


		void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{

			//obtenemos un diccionario con los elementos sem�nticos
			//Definimos las variables internas a usar en la funci�n
			SemanticValue semantics = e.Result.Semantics;
			string rawText = e.Result.Text;
			RecognitionResult result = e.Result;
			string resultValue;

			//Implementamos de forma "cutre" la m�quina de estados
            switch (state)
            {
				case 0:
                    if (semantics.ContainsKey("yn"))
                    {
						resultValue = (String)semantics["yn"].Value;
						synth.Speak("Has dicho que " + resultValue + " ¿eh?");
						switch (resultValue)
                        {
							case "Si":
								//Cambio el estado
								this.state = 1;
								//Desactivo la posibilidad de decir s� o no
								DesactivarGramatica(grammarYesNo);
								synth.Speak("Ahora dime tu nombre");
								//Activo la posibilidad de decir el nombre
								ActivarGramatica(grammarNombres);
							break;
							case "No":
								synth.Speak("Te voy a preguntar hasta que digas que sí.");
								synth.Speak("Eres la única perosna que puede salvarnos...");
								synth.Speak("Y la persona más bella que conozco jeje");
							break;
                        }
                    }
				break;
				case 1:
                    if (semantics.ContainsKey("nom"))
                    {
											//TODO aqui molaria mil que reconociese el nombre cualfuese
						resultValue = (String)semantics["nom"].Value;
						synth.Speak("Estoy encantada de conocerte, " + resultValue);
						DesactivarGramatica(grammarNombres);
						synth.Speak("Ahora escoge un arma con la que salvar el mundo");
						this.state = 2;
                    }
				break;
            }
		}


			private Grammar CreateGrammarColors(params int[] info)
			{
				//synth.Speak("Creando ahora la gram�tica");
				Choices colorChoice = new Choices();

				SemanticResultValue choiceResultValue =
						new SemanticResultValue("Rojo", Color.FromName("Red").ToArgb());
				GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
				colorChoice.Add(resultValueBuilder);

				choiceResultValue =
					   new SemanticResultValue("Azul", Color.FromName("Blue").ToArgb());
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				colorChoice.Add(resultValueBuilder);

				choiceResultValue =
					   new SemanticResultValue("Verde", Color.FromName("Green").ToArgb());
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				colorChoice.Add(resultValueBuilder);

				SemanticResultKey choiceResultKey = new SemanticResultKey("rgb", colorChoice);
				GrammarBuilder colors = new GrammarBuilder(choiceResultKey);


				GrammarBuilder poner = "Poner";
				GrammarBuilder cambiar = "Cambiar";
				GrammarBuilder fondo = "Fondo";

				Choices dos_alternativas = new Choices(poner, cambiar);
				GrammarBuilder frase = new GrammarBuilder(dos_alternativas);
				frase.Append(fondo);
				frase.Append(colors);
				Grammar grammar = new Grammar(frase);
				grammar.Name = "Poner/Cambiar Colores";

				//Grammar grammar = new Grammar("so.xml.txt");

				return grammar;
			}


			private Grammar CreateGrammarName(params int[] info)
			{
				//synth.Speak("Creando ahora la gram�tica");
				Choices nameChoice = new Choices();


				SemanticResultValue choiceResultValue =
						new SemanticResultValue("Paco", "Francisco");
				GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
				nameChoice.Add(resultValueBuilder);

				choiceResultValue =
					   new SemanticResultValue("Pepe", "Jose");
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				nameChoice.Add(resultValueBuilder);

				choiceResultValue =
					   new SemanticResultValue("Rub�n", "Ruqui");
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				nameChoice.Add(resultValueBuilder);


				choiceResultValue =
					   new SemanticResultValue("Claudia", "Mi querida loca del co�o");
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				nameChoice.Add(resultValueBuilder);

				SemanticResultKey choiceResultKey = new SemanticResultKey("nom", nameChoice);
				GrammarBuilder nombres = new GrammarBuilder(choiceResultKey);


				GrammarBuilder poner = "mi nombre es";
				GrammarBuilder cambiar = "me llamo";


				Choices dos_alternativas = new Choices(poner, cambiar);
				GrammarBuilder frase = new GrammarBuilder(dos_alternativas);
				frase.Append(nombres);
				Grammar grammar = new Grammar(frase);
				grammar.Name = "Poner/Cambiar Nombre";

				//Grammar grammar = new Grammar("so.xml.txt");

				return grammar;


			}

		private Grammar CreateGrammarYesNo(params int[] info)
		{
			Choices yes_noChoice = new Choices();
			SemanticResultValue choiceResultValue =
					new SemanticResultValue("Si", "Si");
			GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
			yes_noChoice.Add(resultValueBuilder);

			choiceResultValue =
				   new SemanticResultValue("No", "No");
			resultValueBuilder = new GrammarBuilder(choiceResultValue);
			yes_noChoice.Add(resultValueBuilder);

			SemanticResultKey choiceResultKey = new SemanticResultKey("yn", yes_noChoice);
			GrammarBuilder si_no = new GrammarBuilder(choiceResultKey);

			Grammar grammar = new Grammar(si_no);
			grammar.Name = "Decir si / no";

			return grammar;
		}


	}


	//Todo a�adir estados


}
