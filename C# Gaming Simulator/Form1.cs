using System.Reflection;
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;
using System.Reflection.Metadata;

namespace C__Gaming_Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowLoadingForm()
        {
            Carregamento loadingForm = new Carregamento();

            Thread thread = new Thread(() =>
            {
                Application.Run(loadingForm);
            });

            thread.Start();

            Thread.Sleep(1000);

            loadingForm.Invoke(new Action(() => loadingForm.Close()));
        }

        private void AbrirGame(string path)
        {
            this.Hide();
            ShowLoadingForm();
            string game = $"{Directory.GetParent(Environment.CurrentDirectory)}\\games\\{path}";

            if (System.IO.File.Exists(game))
            {
                try
                {
                    this.Hide();
                    Process processo = new Process();

                    processo.StartInfo.FileName = game;

                    processo.EnableRaisingEvents = true;
                    processo.Exited += (sender, e) =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Show();
                        });
                        processo.Dispose();
                    };

                    processo.Start();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao abrir o jogo: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Jogo não localizado.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirGame("cobrinha\\Cobrinha.exe");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AbrirGame("Corridadecarros\\CorridaDeCarros.exe");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirGame("turnOfLegends\\ProjetoFinal.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirGame("pptls\\pptls.exe");
        }
    }
}