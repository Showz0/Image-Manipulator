/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
 *Curso Técnico em Informática - Data de Entrega: 08 / 09 / 2024
* Autores do Projeto: Vitor Serpa da Silva e Marcus Aurelius Vitoriano Silva
*
* Turma: 3F
* Projeto ICG 3° Bimestre
* 
* 
* ******************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace processamentoImagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap imagemFundo = new Bitmap(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24\Im.jpg");
        Bitmap imagemAviao = new Bitmap(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24\Aviao2.jpg");
        Bitmap imagemHomem = new Bitmap(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24\homem.jpg");
        Bitmap imagemBalao = new Bitmap(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24\Balao.jpg");

        public Bitmap filtroBinario(Bitmap imagem)
        {
            Bitmap imagemCinza = filtroCinza(imagem);
            int alturaImagem = imagemCinza.Height;
            int larguraImagem = imagemCinza.Width;
            Bitmap novaImagem = new Bitmap(larguraImagem, alturaImagem);

            for (int i = 0; i < larguraImagem; i++)
            {
                for (int j = 0; j < alturaImagem; j++)
                {
                    Color cor = imagemCinza.GetPixel(i, j);
                    if (cor.R <= 126)
                    {
                        novaImagem.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        novaImagem.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            return novaImagem;
        }

        public Bitmap filtroCinza(Bitmap imagem)
        {
            int alturaImagem = imagem.Height;
            int larguraImagem = imagem.Width;
            Bitmap novaImagem = new Bitmap(larguraImagem, alturaImagem);

            for (int i = 0; i < larguraImagem; i++)
            {
                for (int j = 0; j < alturaImagem; j++)
                {
                    Color cor = imagem.GetPixel(i, j);
                    int grey = (int)(cor.R * 0.30 + cor.G * 0.59 + cor.B * 0.11);
                    novaImagem.SetPixel(i, j, Color.FromArgb(grey, grey, grey));
                }
            }
            return novaImagem;
        }

        public Bitmap filtroBrilho(Bitmap imagem, int brilho)
        {
            int alturaImagem = imagem.Height;
            int larguraImagem = imagem.Width;
            Bitmap novaImagem = new Bitmap(larguraImagem, alturaImagem);

            for (int i = 0; i < larguraImagem; i++)
            {
                for (int j = 0; j < alturaImagem; j++)
                {
                    Color cor = imagem.GetPixel(i, j);
                    int rNovo = Math.Max(0, Math.Min(cor.R + brilho, 255));
                    int gNovo = Math.Max(0, Math.Min(cor.G + brilho, 255));
                    int bNovo = Math.Max(0, Math.Min(cor.B + brilho, 255));

                    novaImagem.SetPixel(i, j, Color.FromArgb(rNovo, gNovo, bNovo));
                }
            }
            return novaImagem;
        }

        public void juntarImagem(Bitmap imagemFundo, Bitmap imagemCima, int posicaoX, int posicaoY)
        {
            int alturaImagemCima = imagemCima.Height;
            int larguraImagemCima = imagemCima.Width;
            Color primeiroPixel = imagemCima.GetPixel(1, 1);

            for (int i = 0; i < larguraImagemCima; i++)
            {
                for (int j = 0; j < alturaImagemCima; j++)
                {
                    Color imagemCimaColor = imagemCima.GetPixel(i, j);

                    if (!(calculoToleranciaCor(primeiroPixel, imagemCimaColor, 30)))
                    {
                        imagemFundo.SetPixel(posicaoX + i, posicaoY + j, imagemCimaColor);
                    }
                }
            }
        }

        public Bitmap transporImagem(Bitmap imagem)
        {
            int larguraImagem = imagem.Width;
            int alturaImagem = imagem.Height;
            Bitmap novaImagem = new Bitmap(alturaImagem, larguraImagem);

            for (int x = 0; x < larguraImagem; x++)
            {
                for (int y = 0; y < alturaImagem; y++)
                {
                    Color corPixel = imagem.GetPixel(x, y);
                    novaImagem.SetPixel(y, larguraImagem - x - 1, corPixel);
                }
            }

            return novaImagem;
        }

        public Boolean calculoToleranciaCor(Color corPrimeiroPixel, Color corAtual, int tolerancia)
        {
            int R = Math.Abs(corPrimeiroPixel.R - corAtual.R);
            int G = Math.Abs(corPrimeiroPixel.G - corAtual.G);
            int B = Math.Abs(corPrimeiroPixel.B - corAtual.B);

            return R < tolerancia && G < tolerancia && B < tolerancia;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            juntarImagem(imagemFundo, imagemBalao, 550, 40);
            juntarImagem(imagemFundo, imagemHomem, 780, 580);
            juntarImagem(imagemFundo, imagemAviao, 130, 80);
            pictureBox4.Image = imagemFundo;

            imagemFundo.Save(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24" + "imagemJunta.jpg");

            Bitmap imagemTransposta = transporImagem(imagemFundo);
            pictureBox5.Image = imagemTransposta;
            imagemTransposta.Save(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24" + "imagemTransposta.jpg");

            Bitmap imagemCinza = filtroCinza(imagemFundo);
            pictureBox2.Image = imagemCinza;
            imagemCinza.Save(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24" + "imagemCinza.jpg");

            Bitmap imagemBinario = filtroBinario(imagemFundo);
            pictureBox1.Image = imagemBinario;
            imagemBinario.Save(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24" + "imagemBinaria.jpg");

            Bitmap imagemBrilho = filtroBrilho(imagemFundo, 85);
            pictureBox3.Image = imagemBrilho;
            imagemBrilho.Save(@"C:\Users\marcu\Downloads\Imgprojeto_3Bim_24" + "imagemBrilho.jpg");


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
