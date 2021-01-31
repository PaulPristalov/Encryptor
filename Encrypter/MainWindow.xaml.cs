using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Encrypter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            Encrypt();
        }
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Encrypt();
            if (e.Key == Key.Escape)
                Input.Text = "";
        }

        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Window move
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Encrypt()
        {
            // Get entered text
            string text = Input.Text;
            // Spliting into words 
            string[] words = text.Split();
            // Result
            string encryptedText = "";

            foreach (var word in words)
            {
                // Spliting into chars
                char[] chars = word.ToCharArray();

                int posInWord = 0;
                foreach (var ch in chars)
                {
                    char[] curAlph;
                    // Char to lower
                    char curChar = ch.ToString().ToLower().ToCharArray()[0];

                    if (Alphabets.Eng.Contains(curChar))
                        curAlph = Alphabets.Eng;
                    else if (Alphabets.Rus.Contains(curChar))
                        curAlph = Alphabets.Rus;
                    else
                    {
                        encryptedText += curChar;
                        continue;
                    }

                    // Position in alphabet of current char
                    int posInAlph = 0;
                    while (curAlph[posInAlph] != curChar)
                        posInAlph++;

                    // Encrypting
                    posInWord++;
                    int offset = word.Length - posInWord;

                    // Final position
                    int finPos = Math.Abs(posInAlph - curAlph.Length) + offset;

                    // If position bigger, than alphabet lenght
                    while (finPos >= curAlph.Length)
                        finPos -= curAlph.Length;

                    // If the char was upper
                    if (ch != curChar)
                        encryptedText += curAlph[finPos].ToString().ToUpper().ToCharArray()[0];
                    else
                        encryptedText += curAlph[finPos];
                }

                encryptedText += ' ';
            }

            // Set encrypted text to output block
            Output.Text = encryptedText.Trim();
        }
    }
}
