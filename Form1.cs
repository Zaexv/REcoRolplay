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
	public partial class Form1 : Form
	{
		private System.Speech.Recognition.SpeechRecognitionEngine _recognizer =
		   new SpeechRecognitionEngine();
		private SpeechSynthesizer synth = new SpeechSynthesizer();


		//Variables privadas para usar en las funciones // 
		private Grammar grammarColors, grammarNombres;
		private int state; //El estado del di�logo

		public Form1()
		{
			//Esto pinta el formulario. 
			InitializeComponent();
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			synth.Speak("Estimado ser humano. Has conseguido viajar en tiempo para salvar a la humanidad de la devastaci�n total.");
			//Inicializo la gram�tica y todos sus componentes
			state = 0;
			grammarColors = CreateGrammarColors(null);
			grammarNombres = CreateGrammarName(null);

			//No cambiar, inicializando el recognizer
			_recognizer.SetInputToDefaultAudioDevice();
			_recognizer.UnloadAllGrammars();
			// Nivel de confianza del reconocimiento 70%
			_recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 60);

			//Activo la gram�ticas creadas previamene.
			ActivarGramatica(grammarNombres);
			ActivarGramatica(grammarColors);

			//No cambiar, inicializando el recognizer
			_recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
			//reconocimiento as�ncrono y m�ltiples veces
			_recognizer.RecognizeAsync(RecognizeMode.Multiple);
			synth.Speak("Ahora cu�ntame; �C�mo te llamas?");
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
			SemanticValue semantics = e.Result.Semantics;
			string rawText = e.Result.Text;
			RecognitionResult result = e.Result;

			if (semantics.ContainsKey("nom"))
			{
				String text = "Hola se�or " + semantics["nom"].Value + " �C�mo est�s?";
				synth.Speak(text);
				DesactivarGramatica(grammarColors); 
				this.pictureBox1.Visible = false;
				this.label1.Text = (String)semantics["nom"].Value; 
			}
			else if (semantics.ContainsKey("rgb"))
			{
				this.label1.Text = rawText;
				this.pictureBox1.Visible = true;
				this.BackColor = Color.FromArgb((int)semantics["rgb"].Value);
				String texto = "Est�s cambiando el color, ten cuidao, has cambiao el color a: " + (int)semantics["rgb"].Value;
				//synth.Speak(texto);
				Update();
				//synth.Speak(rawText);
			}
			//TODO hacer que entre en esto cuando el programa no te entiende. 
			else
			{
				synth.Speak("No te he entendido claramente; �Podr�as repetirlo?");
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
		}


	//Todo a�adir estados 


	}
