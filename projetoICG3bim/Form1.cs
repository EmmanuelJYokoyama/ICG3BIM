using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace projetoICG3bim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //-------------------- VARIAVEIS --------------------

        int coluna = 0;
        int linha = 0;
        Color cor;
        Bitmap imgCozinha = new Bitmap("C:\\Users\\emman\\Pictures\\Imagem_A.jpg");
        Bitmap imgPanela = new Bitmap("C:\\Users\\emman\\Pictures\\Panela.jpg");

        //-------------------- IMAGEM CINZA --------------------

        public Color CriaCor(byte a, byte r, byte g, byte b)
        {
            Color Cor = new Color();
            Cor = Color.FromArgb(a, r, g, b);
            return Cor;
        }


        private Bitmap Cinza ()
        {
            imgCozinha = SobrepoeImagem();
            coluna = imgCozinha.Width; // O número colunas 
            linha = imgCozinha.Height; // O número de linhas
            Bitmap imgnova = new Bitmap(coluna, linha); 
            cor = new Color();
            for (int i = 0; i <= coluna - 1; i++)
            {
                for (int j = 0; j <= linha - 1; j++)
                {
                    byte r = imgCozinha.GetPixel(i, j).R;
                    byte g = imgCozinha.GetPixel(i, j).G;
                    byte b = imgCozinha.GetPixel(i, j).B;

                    byte K = (byte)(r * 0.3 + g * 0.59 + b * 0.11);

                    Color cor = CriaCor(255, K, K, K);
                    imgnova.SetPixel(i, j, cor);

                }
            }
            imgnova.Save("C:\\users\\emman\\Pictures\\IMG_CINZA.jpg");
            return imgnova;
        }

        //---------------------------------------------------------------------

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            /*****************************************/
        }

        //---------------------------------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = imgCozinha;
        }

        //-------------------- IMAGEM P/B --------------------

        private Bitmap pretoBranco()
        {
            imgCozinha = SobrepoeImagem();
            coluna = imgCozinha.Width; // O número colunas 
            linha = imgCozinha.Height; // O número de linhas
            Bitmap imgnova = new Bitmap(coluna, linha);
            cor = new Color();
            for (int i = 0; i <= coluna - 1; i++)
            {
                for (int j = 0; j <= linha - 1; j++)
                {
                    byte r = imgCozinha.GetPixel(i, j).R;
                    byte g = imgCozinha.GetPixel(i, j).G;
                    byte b = imgCozinha.GetPixel(i, j).B;

                    byte K = (byte)(r * 0.3 + g * 0.59 + b * 0.11);

                    if (K >= 127)
                        K = 255;
                    else
                        K = 0;

                    Color cor = CriaCor(255, K, K, K);
                    imgnova.SetPixel(i, j, cor);

                }
            }
            imgnova.Save("C:\\users\\emman\\Pictures\\IMG_B&W.jpg");
            return imgnova;

        }

        //---------------------------------------------------------------------

        private Bitmap tiraAmarelo()
        {
            int coluna = imgPanela.Width; // O número colunas 
            int linha = imgPanela.Height; // O número de linhas
            Bitmap imgnova = new Bitmap(coluna, linha);
            Color cor = new Color();
            for (int i = 0; i <= coluna - 1; i++)
            {
                for (int j = 0; j <= linha - 1; j++)
                {
                    byte r = imgPanela.GetPixel(i, j).R;
                    byte g = imgPanela.GetPixel(i, j).G;
                    byte b = imgPanela.GetPixel(i, j).B;

                    if ((r + g) / 2 >= 220 && (r + g + b) / 3 <= 230)
                    {
                        imgnova.SetPixel(i, j, CriaCor(0, 0, 0, 0));
                    }
                    else
                    {
                        cor = CriaCor(255, r, g, b);
                        imgnova.SetPixel(i, j, cor);
                    }

                }
            }
            return imgnova;
        }

        //---------------------------------------------------------------------

        private Bitmap SobrepoeImagem()
        {
            Bitmap panela = tiraAmarelo();
            Bitmap img_resultado = new Bitmap(imgCozinha);
            int cont1 = 150;

            for (int y = 0; y < panela.Width; y++)
            {
                cont1++;
                int cont2 = 0;
                for (int x = 0; x < panela.Height; x++)
                {
                    cont2++;
                    Color pixelSobre = panela.GetPixel(y, x);

                    // Se a cor do pixel da sobreposição não for transparente, aplicar sobreposição
                    if (pixelSobre.A != 0)
                    {
                        img_resultado.SetPixel(cont1, cont2, pixelSobre);
                    }
                }
            }
            img_resultado.Save("C:\\users\\emman\\Pictures\\IMG_SOBREPOSTA.jpg");
            return img_resultado;
            
        }

        //-------------------- SOBREPOR IMG ---------------------

        private void button4_Click(object sender, EventArgs e)
        {
            //Bitmap img_resultado = SobrepoeImagem();
            pictureBox3.Image = SobrepoeImagem();
            pictureBox4.Image = Cinza();
            pictureBox5.Image = pretoBranco();
            

            
        }
    }
}
