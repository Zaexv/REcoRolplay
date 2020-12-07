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
using System.Media;

namespace REcoSample
{
	public partial class Formulario_Rolplay : Form
	{
		private System.Speech.Recognition.SpeechRecognitionEngine _recognizer =
		   new SpeechRecognitionEngine();
		private SpeechSynthesizer synth = new SpeechSynthesizer();


		//Variables privadas para usar en las funciones //
		private Grammar grammarColors, grammarNombres, grammarYesNo, grammarWeapons, grammarShoot, grammarSniper;
		private int state; //El estado del dialogo

		public Formulario_Rolplay()
		{
			//Esto pinta el formulario.
			InitializeComponent();
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			//Inicializo este formulario
			pictureBoxIA.Enabled = true;
			pictureBoxGameOver.Visible = false;
			pictureBoxGameOver.BackColor = Color.Transparent;
			this.Enabled = true;

			synth.Speak("Estimado ser humano. Has conseguido viajar a lo largo del tiempo para salvar a la humanidad de la devastacion total.");
			//Inicializo las gramatica y todos sus componentes

			//Inicializo la variable global de estado
			state = 0;
			

			//Creo las gramaticas con las funciones para crearlas
			grammarColors = CreateGrammarColors(null);
			grammarNombres = CreateGrammarName(null);
			grammarYesNo = CreateGrammarYesNo(null);
			grammarWeapons = CreateGrammarWeapons(null);
			grammarShoot = CreateGrammarShoot(null);
			grammarSniper = CreateGrammarSniper(null);

			//Sonidos
			
			

			//No cambiar, inicializando el recognizer
			_recognizer.SetInputToDefaultAudioDevice();
			_recognizer.UnloadAllGrammars();

			_recognizer.LoadGrammar(grammarColors);
			_recognizer.LoadGrammar(grammarNombres);
			_recognizer.LoadGrammar(grammarYesNo);
			_recognizer.LoadGrammar(grammarWeapons);
			_recognizer.LoadGrammar(grammarShoot);
			_recognizer.LoadGrammar(grammarSniper);
			// Nivel de confianza del reconocimiento 60%
			_recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 60);

			//Activo la gram�ticas creadas previamente.
			//ActivarGramatica(grammarNombres);
			//ActivarGramatica(grammarColors);
			ActivarGramatica(grammarYesNo);

			//No cambiar, inicializando el recognizer
			_recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
			//reconocimiento asincrono y multiples veces
			_recognizer.RecognizeAsync(RecognizeMode.Multiple);
			synth.Speak("Ahora cuentame; ¿Quieres participar en nuestra lucha?");
		}

		//Activa una gram�tica para ser usada
		private void ActivarGramatica(Grammar grammar)
		{
			grammar.Enabled = true;
		}

		//Desactiva una gram�tica para ser usada
		private void DesactivarGramatica(Grammar grammar)
        {
			grammar.Enabled = false;
        }
		
		//Mostrar imagen
		private void ActivarImagen(PictureBox imagen)
		{
			imagen.Enabled = true;
			imagen.Visible = true;
		}

		//Ocultar imagen
		private void DesactivarImagen(PictureBox imagen)
		{
			imagen.Enabled = false;
			imagen.Visible = false;
		}


		void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{

			//obtenemos un diccionario con los elementos sem�nticos
			//Definimos las variables internas a usar en la funci�n
			SemanticValue semantics = e.Result.Semantics;
			string rawText = e.Result.Text;
			RecognitionResult result = e.Result;
			string resultValue;
			Console.WriteLine(rawText); //Pinto el RawText para debuggear

			//Implementamos de forma "cutre" la m�quina de estados
			switch (state)
            {
				case 0:
                    if (semantics.ContainsKey("yn"))
                    {
						resultValue = (String)semantics["yn"].Value;
						synth.Speak("Has dicho que " + resultValue + ", ¿eh?");
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
								synth.Speak("Eres la única persona que puede salvarnos...");
								synth.Speak("Y la más bella que conozco...");
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
						pictureBoxIA.Image = Properties.Resources.weapons;
						ActivarGramatica(grammarWeapons);
						this.state = 2;
                    }
				break;
				case 2:
					if (semantics.ContainsKey("wea"))
                    {
						pictureBoxIA.Image = Properties.Resources.ia;
						resultValue = (String)semantics["wea"].Value;
						synth.Speak("Has elegido tu " + resultValue + " .");

                        switch (resultValue)
                        {
							case "blaster":
								pictureBoxIA.Image = Properties.Resources.blaster;
								synth.Speak("Cuidado, ahí se encuentra el peligroso Mandalorian, quieres dispararle con tu blaser?");
								DesactivarGramatica(grammarWeapons);
								ActivarGramatica(grammarYesNo);
								this.state = 3; 

							break;
							case "sniper":
								synth.Speak("Indiana Jones se encuentra capturado en alguno de estos dos vehiculos. Quieres disparar al vehiculo de la izquierda o de la derecha?");
								pictureBoxIA.Image = Properties.Resources.francotirador;
								DesactivarGramatica(grammarWeapons);
								ActivarGramatica(grammarSniper);
								this.state = 4; 
							break;
							case "pistola":
								pictureBoxIA.Image = Properties.Resources.chucknorris;
								synth.Speak("Te toca enfrentarte al gran Chuck Norris, en donde le vas a disparar?");
								DesactivarGramatica(grammarWeapons);
								ActivarGramatica(grammarShoot);
								this.state = 9; 
							break;
                        }
						DesactivarGramatica(grammarWeapons);
						
						
					}
				break;
				case 3:
					if (semantics.ContainsKey("yn"))
					{
						resultValue = (String)semantics["yn"].Value;
						synth.Speak("Has dicho que " + resultValue + ", ¿eh?");
						switch (resultValue)
						{
							case "Si":
								ActivarImagen(pictureBoxBabyYoda);
								synth.Speak("Oh no, Baby Yoda acaba de aparecer y utilizó la fuerza para derrotarte");
								DesactivarImagen(pictureBoxBabyYoda);
								DesactivarGramatica(grammarYesNo);
								pictureBoxIA.Image = Properties.Resources.gameover2;
								System.Media.SoundPlayer gameOverMusic = new System.Media.SoundPlayer(Properties.Resources.gameOverMusic);
								gameOverMusic.Play();



								synth.Speak("Game over.");
								break;
							case "No":
								ActivarImagen(pictureBoxBabyYoda);
								synth.Speak("Excelente elección, con Baby Yoda de nuestro lado seremos capaces de salvar al mundo");
								break;
						}
					}
					break;
				case 4:
					if (semantics.ContainsKey("sn"))
					{
						resultValue = (String)semantics["sn"].Value;
						synth.Speak("Has dicho " + resultValue + ", ¿eh?");
						switch (resultValue)
						{
							case "Izquierda":
								DesactivarGramatica(grammarSniper);
								pictureBoxIA.Image = Properties.Resources.choqueVehiculo;
								wait(5000);
								synth.Speak("Oh no, acabas de destruir el vehiculo de Indiana Jones, ya no podra ayudarnos a salvar a la humanidad");
								synth.Speak("Game Over");
								pictureBoxIA.Image = Properties.Resources.gameover2;
								break;
							case "Derecha":
								DesactivarGramatica(grammarSniper);
								pictureBoxIA.Image = Properties.Resources.indianaJonesASalvo;
								wait(3000);
								pictureBoxIA.Image = Properties.Resources.IndianaJones;
								wait(1000);
								synth.Speak("Muy buen disparo!");
								synth.Speak("Acabas de salvar a Indiana Jones, y con su ayuda conseguiremos salvar al mundo.");
								break;
						}
					}
					break;
				case 9:
					synth.Speak("Por mucho que seas un viajero del tiempo no puedes matar a Chuck Norris, has perdido");
					pictureBoxGameOver.Image = Properties.Resources.gameover;
					pictureBoxGameOver.Enabled = true;
					pictureBoxGameOver.Visible = true;
					synth.Speak("Game over.");
				break; 
            }
		}

		private Grammar CreateGrammarShoot(params int[] info)
		{
			//Todo Pulir los tipos de Disparo
			Choices shootChoice = new Choices();

			SemanticResultValue choiceResultValue =
					new SemanticResultValue("cabeza", "cabeza");
			GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
			shootChoice.Add(resultValueBuilder);

			choiceResultValue =
				   new SemanticResultValue("hombre", "hombre");
			resultValueBuilder = new GrammarBuilder(choiceResultValue);
			shootChoice.Add(resultValueBuilder);

			choiceResultValue =
				   new SemanticResultValue("pistola", "pistola");
			resultValueBuilder = new GrammarBuilder(choiceResultValue);
			shootChoice.Add(resultValueBuilder);

			SemanticResultKey choiceResultKey = new SemanticResultKey("sht", shootChoice);
			GrammarBuilder shoot = new GrammarBuilder(choiceResultKey);


			GrammarBuilder disparar = "disparar";
			GrammarBuilder matar = "matar";
			GrammarBuilder elegir = "elegir";
			GrammarBuilder apuntar = "apuntar";
			//Todo sería ideal que funcione la cadena vacía.
			Choices cuatro_alternativas = new Choices(elegir, disparar, matar, apuntar);
			GrammarBuilder frase = new GrammarBuilder(cuatro_alternativas);
			frase.Append(shoot);

			Grammar grammar = new Grammar(frase);
			grammar.Name = "Elegir Disparo";

			return grammar;
		}

        private Grammar CreateGrammarWeapons(params int[] info)
		{
			//synth.Speak("Creando ahora la gram�tica");
			Choices weaponChoice = new Choices();

			SemanticResultValue choiceResultValue =
					new SemanticResultValue("blaster", "blaster");
			GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
			weaponChoice.Add(resultValueBuilder);

			choiceResultValue =
				   new SemanticResultValue("sniper", "sniper");
			resultValueBuilder = new GrammarBuilder(choiceResultValue);
			weaponChoice.Add(resultValueBuilder);

			choiceResultValue =
				   new SemanticResultValue("pistola", "pistola");
			resultValueBuilder = new GrammarBuilder(choiceResultValue);
			weaponChoice.Add(resultValueBuilder);

			SemanticResultKey choiceResultKey = new SemanticResultKey("wea", weaponChoice);
			GrammarBuilder weapons = new GrammarBuilder(choiceResultKey);


			GrammarBuilder seleccionar = "seleccionar";
			GrammarBuilder escoger = "escoger";
			GrammarBuilder coger = "coger";
			GrammarBuilder elegir = "elegir";

			Choices cuatro_alternativas = new Choices(elegir, seleccionar, escoger, coger);
			GrammarBuilder frase = new GrammarBuilder(cuatro_alternativas);
			frase.Append(weapons);
			Grammar grammar = new Grammar(frase);
			grammar.Name = "Seleccionar Arma";

			return grammar;
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

				return grammar;
			}


			private Grammar CreateGrammarName(params int[] info)
			{
				//synth.Speak("Creando ahora la gramatica");
				Choices nameChoice = new Choices();


				SemanticResultValue choiceResultValue =
						new SemanticResultValue("Eduardo", "Eduardo");
				GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
				nameChoice.Add(resultValueBuilder);

				choiceResultValue =
					   new SemanticResultValue("Andrés", "Andrés");
				resultValueBuilder = new GrammarBuilder(choiceResultValue);
				nameChoice.Add(resultValueBuilder);

				choiceResultValue =
					   new SemanticResultValue("Francisco", "Francisco");
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

		private Grammar CreateGrammarSniper(params int[] info)
		{
			Choices sniperChoice = new Choices();
			SemanticResultValue choiceResultValue =
					new SemanticResultValue("Izquierda", "Izquierda");
			GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
			sniperChoice.Add(resultValueBuilder);

			choiceResultValue =
				   new SemanticResultValue("Derecha", "Derecha");
			resultValueBuilder = new GrammarBuilder(choiceResultValue);
			sniperChoice.Add(resultValueBuilder);

			SemanticResultKey choiceResultKey = new SemanticResultKey("sn", sniperChoice);
			GrammarBuilder sniper = new GrammarBuilder(choiceResultKey);

			Grammar grammar = new Grammar(sniper);
			grammar.Name = "Disparar izquierda / derecha";

			return grammar;
		}

		public void wait(int milliseconds)
		{
			var timer1 = new System.Windows.Forms.Timer();
			if (milliseconds == 0 || milliseconds < 0) return;

			// Console.WriteLine("start wait timer");
			timer1.Interval = milliseconds;
			timer1.Enabled = true;
			timer1.Start();

			timer1.Tick += (s, e) =>
			{
				timer1.Enabled = false;
				timer1.Stop();
				// Console.WriteLine("stop wait timer");
			};

			while (timer1.Enabled)
			{
				Application.DoEvents();
			}
		}


	}


}
