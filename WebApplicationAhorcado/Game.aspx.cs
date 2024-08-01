using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplicationAhorcado
{
    public partial class Game : System.Web.UI.Page
    {
        private string RandomWord
        {
            get { return Session["RandomWord"].ToString(); }
            set { Session["RandomWord"] = value; }
        }
        private int Attempts
        {
            get { return (int)Session["Attempts"]; }
            set { Session["Attempts"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateWord();
                Session["GuessedLetters"] = "";
                Attempts = 5;
            }

            AttemptsLabel.Text = "Attempts: " + Attempts;
            GenerateButtons();
            GenerateBoxes();
        }

        private void GenerateWord()
        {
            List<String> words = new List<string>
            { "Manzana", "Programacion", "Picaporte", "Helado", "Musica"};

            Random random = new Random();
            int randomIndex = random.Next(words.Count);
            RandomWord = words[randomIndex];
        }

        private void GenerateButtons()
        {
            LettersPanel.Controls.Clear();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                Button letterButton = new Button();
                letterButton.ID = "Button" + c;
                letterButton.Text = c.ToString();
                letterButton.Click += new EventHandler(LetterButton_Click);
                letterButton.CssClass = "letter-button";

                letterButton.Attributes.Add("class", "letter-button");

                LettersPanel.Controls.Add(letterButton);
            }
        }

        private void GenerateBoxes()
        {
            BoxesPanel.Controls.Clear();

            for (int i = 0; i < RandomWord.Length; i++)
            {
                Literal literal = new Literal();
                literal.ID = "Box" + (i + 1);
                literal.Text = "<span class='box-class'>" + GetSpanText(i) + "</span>";

                BoxesPanel.Controls.Add(literal);
            }
        }

        private string GetSpanText(int index)
        {
            string guessedLetters = Session["GuessedLetters"] as string;
            char letter = RandomWord[index];

            if (guessedLetters != null)
            {
                foreach (char guessedLetter in guessedLetters.ToUpper())
                {
                    if (guessedLetter == char.ToUpper(letter))
                        return letter.ToString().ToUpper();
                }
            }
            return "";
        }

        protected void LetterButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string letter = clickedButton.Text;

            string guessedLetters = Session["GuessedLetters"] as string;

            if (guessedLetters == null)
                guessedLetters = letter;
            else
                guessedLetters += letter;

            Session["GuessedLetters"] = guessedLetters;

            bool correctGuess = false;
            
            for (int i = 0; i < RandomWord.Length; i++)
            {
                char wordLetter = RandomWord[i];
                if (char.ToUpper(wordLetter) == char.ToUpper(letter[0]))
                {
                    correctGuess = true;
                    break;
                }
            }

            if (!correctGuess)
            {
                Attempts--;
                AttemptsLabel.Text = "Attempts: " + Attempts;

                if (Attempts == 0)
                {
                    AttemptsLabel.Text = "GAME OVER :(";
                    DisableAllButtons();
                }
            }
            else
            {
                clickedButton.Enabled = false;

                int correctGuesses = 0;
                foreach (char c in RandomWord)
                {
                    if (guessedLetters.ToUpper().Contains(char.ToUpper(c)))
                        correctGuesses++;
                }

                if (correctGuesses == RandomWord.Length)
                    AttemptsLabel.Text = "YOU WIN!!!";
            }

            GenerateBoxes();
        }

        private void DisableAllButtons()
        {
            foreach (Control control in LettersPanel.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.Enabled = false;
                }
            }
        }
    }
}