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

namespace Calculadora
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ans = 0;
        List<int> operandos = new List<int>();
        List<int> operandosFinales = new List<int>();
        char[] operadores = { '+', '-','*','/'};
        List<char> operadoresUsados = new List<char>();
        List<char> operadoresFinales = new List<char>();
        int resultadoFinal = 0;
        bool igualPresionado = false;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Añadir_Numero(object sender, RoutedEventArgs e)
        {

            Button botonenviado = (Button)sender;
            string contenido;

            if ((String)botonenviado.Content == "ANS")
            {
                contenido = ans.ToString();
            }
            else
            {
                contenido = (string)botonenviado.Content;
            }

            if (pantalla.Text == "0")
            {
                pantalla.Text = contenido;

            }
            else
            {
                pantalla.Text += contenido;
                
            }
            
        }

        private void Borrar_Todo(object sender, RoutedEventArgs e)
        {
            Reiniciar();
        }

        private void Borrar_Ultimo_Digito(object sender, RoutedEventArgs e)
        {

            pantalla.Text = pantalla.Text.Remove(pantalla.Text.Length - 1);

            if (pantalla.Text == "")
            {

                pantalla.Text = "0";

            }            

                
        }

        private void Delimitar_Operando(object sender, RoutedEventArgs e)
        {

            Button botonEnviado = (Button)sender;
            pantalla.Text += botonEnviado.Content;

            Agregar_Operandos();

            pantallaOperandos.Text += pantalla.Text;
            pantalla.Text = "0";
    
        }

        private void Calcular_Resultado(object sender, RoutedEventArgs e )
        {
            if (igualPresionado == false)
            {
                Button botonEnviado = (Button)sender;
                pantalla.Text += botonEnviado.Content;
                pantallaOperandos.Text += pantalla.Text;

                Agregar_Operandos();

                Calcular_Cantidad_Operadores();


                if (operadoresUsados.Count >= 1)
                {


                    for (int i = 0; i < operadoresUsados.Count; i++)
                    {

                        switch (operadoresUsados[i])
                        {

                            case '*':

                                if (operandos[i] == 0)
                                {
                                    try
                                    {
                                        operandosFinales.Add(operandos[i + 1] * operandosFinales[0]);
                                    }
                                    catch (Exception )
                                    {
                                        operandosFinales.Add(0);
                                        i += 2;
                                        break;
                                    }
                                    
                                    operandosFinales.RemoveAt(0);
                                    operandos.RemoveAt(i + 1);
                                    operandos.Insert(i + 1, 0);
                                    operadoresUsados.RemoveAt(i);
                                    operadoresUsados.Insert(i, '?');
                                    break;
                                }
                                else
                                {
                                    operandosFinales.Add(operandos[i] * operandos[i + 1]);
                                    operandos.RemoveAt(i);
                                    operandos.RemoveAt(i);
                                    operandos.Insert(i, 0);
                                    operandos.Insert(i, 0);
                                    operadoresUsados.RemoveAt(i);
                                    operadoresUsados.Insert(i, '?');
                                    break;
                                }

                                

                            case '/':

                                if (operandos[i] == 0)
                                {
                                    try
                                    {
                                        operandosFinales.Add(operandosFinales[0] / operandos[i + 1]);
                                    }
                                    catch (Exception)
                                    {
                                        pantallaOperandos.Text = "No puedes dividir entre cero";
                                        operandosFinales.Add(0);

                                    }

                                    operandosFinales.RemoveAt(0);
                                    operandos.RemoveAt(i + 1);
                                    operandos.Insert(i + 1, 0);
                                    operadoresUsados.RemoveAt(i);
                                    operadoresUsados.Insert(i, '?');
                                    break;
                                }
                                else
                                {
                                    try
                                    {
                                        operandosFinales.Add(operandos[i] / operandos[i + 1]);
                                    }
                                    catch (Exception )
                                    {

                                        pantallaOperandos.Text = "No puedes dividir entre cero";
                                        operandosFinales.Add(0);
                                    }
                                    
                                    operandos.RemoveAt(i);
                                    operandos.RemoveAt(i);
                                    operandos.Insert(i, 0);
                                    operandos.Insert(i, 0);
                                    operadoresUsados.RemoveAt(i);
                                    operadoresUsados.Insert(i, '?');
                                    break;
                                }

                            default:
                                break;
                        }
                    }

                    for (int i = 0; i < operadoresUsados.Count; i++)
                    {
                        if (operadoresUsados[i] == '+' || operadoresUsados[i] == '-')
                        {
                            operadoresFinales.Add(operadoresUsados[i]);
                        }
                    }

                    for (int i = 0; i < operandos.Count; i++)
                    {
                        if (operandos[i] > 0)
                        {
                            operandosFinales.Add(operandos[i]);
                        }
                    }

                    resultadoFinal = operandosFinales[0];

                    if (operadoresFinales.Count > 0)
                    {

                        for (int i = 1; i < operandosFinales.Count; i++)
                        {
                            switch (operadoresFinales[i - 1])
                            {

                                case '+':

                                    resultadoFinal += operandosFinales[i];
                                    break;

                                case '-':

                                    resultadoFinal -= operandosFinales[i];
                                    break;

                                default:

                                    resultadoFinal += operandosFinales[i];
                                    break;
                            }
                        }

                    }
                    else
                    {

                        resultadoFinal = operandosFinales[0];

                    }
                    

                }

                ans = resultadoFinal;
                resultadoFinal = 0;
                pantalla.Text = ans.ToString();
                operadoresUsados.Clear();
                igualPresionado = true;
                
            }
            else if (igualPresionado == true)
            {
                Reiniciar();
            }
            

        }

        private void Agregar_Operandos()
        {
            string operando = "";

            for (int i = 0; i < pantalla.Text.Length - 1; i++)
            {
                operando += pantalla.Text[i];
            }

            int auxiliar = Convert.ToInt32(operando);

            operandos.Add(auxiliar);


        }

        private void Calcular_Cantidad_Operadores()
        {

            for (int i = 0; i < pantallaOperandos.Text.Length; i++)
            {

                for (int x = 0; x < operadores.Length; x++)
                {
                   
                    if (pantallaOperandos.Text[i].Equals(operadores[x]))
                    {
                        operadoresUsados.Add(pantallaOperandos.Text[i]);
                    }
                }
                
            }

        }

        private void Reiniciar()
        {
            pantalla.Text = "0";
            pantallaOperandos.Text = "";
            operandos.Clear();
            operadoresUsados.Clear();
            resultadoFinal = 0;
            operadoresFinales.Clear();
            operandosFinales.Clear();
            igualPresionado = false;

        }
    }
}
    