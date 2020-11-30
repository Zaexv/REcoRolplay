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

		public Form1()
		{
			//Esto pinta el formulario. 
			InitializeComponent();
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			//Aquí se carga el formulario 1) 

			synth.Speak("Bienvenido al diseño de interfaces avanzadas. Inicializando la Aplicación");

			//Inicializo la gramática y todos sus componentes
			Grammar grammar = CreateGrammarColors(null);
			Grammar grammarNombres = CreateGrammarTest(null);

			//No cambiar, inicializando el recognizer
			_recognizer.SetInputToDefaultAudioDevice();
			_recognizer.UnloadAllGrammars();
			// Nivel de confianza del reconocimiento 70%
			_recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 50);

			//Activo la gramáticas creadas previamene.
			ActivarGramatica(grammarNombres);
			ActivarGramatica(grammar);

			//No cambiar, inicializando el recognizer
			_recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
			//reconocimiento asíncrono y múltiples veces
			_recognizer.RecognizeAsync(RecognizeMode.Multiple);
			synth.Speak("Aplicación preparada para reconocer su voz");
		}

		private void ActivarGramatica(Grammar grammar)
		{
			grammar.Enabled = true;
			_recognizer.LoadGrammar(grammar);
		}



		void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{

			//obtenemos un diccionario con los elementos semánticos
			SemanticValue semantics = e.Result.Semantics;
			string rawText = e.Result.Text;
			RecognitionResult result = e.Result;


			if (semantics.ContainsKey("nom"))
			{
				String text = "Hola señor " + semantics["nom"].Value + " ¿Cómo estás?";
				synth.Speak(text);
				Thread.Sleep(1000);
			}
			else if (semantics.ContainsKey("rgb"))
			{
				this.label1.Text = rawText;
				this.BackColor = Color.FromArgb((int)semantics["rgb"].Value);
				String texto = "Estás cambiando el color, ten cuidao, has cambiao el color a: " + (int)semantics["rgb"].Value;
				//synth.Speak(texto);
				Update();
				//synth.Speak(rawText);
			}
			else
			{
				synth.Speak("Illo, que no te entiendo");
			}
		}


			private Grammar CreateGrammarColors(params int[] info)
			{
				//synth.Speak("Creando ahora la gramática");
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


			private Grammar CreateGrammarTest(params int[] info)
			{
				//synth.Speak("Creando ahora la gramática");
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
					   new SemanticResultValue("Rubén", "Ruqui");
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				nameChoice.Add(resultValueBuilder);

				SemanticResultKey choiceResultKey = new SemanticResultKey("nom", nameChoice);
				GrammarBuilder nombres = new GrammarBuilder(choiceResultKey);


				GrammarBuilder poner = "mi nombre es ";
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
	}
