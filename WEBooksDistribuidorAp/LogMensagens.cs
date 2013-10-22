using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Messaging;
using System.Threading;
using System.Collections;
using System.Xml;


namespace WEBooksDistribuidorAp
{
    public partial class LogMensagens : Form
    {
        private System.Messaging.MessageQueue mqDist;
        private System.Messaging.MessageQueue mqWeb;

        public string escreveNoLog = null;

        public LogMensagens()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            
            //Q Creation
            if (MessageQueue.Exists(@".\Private$\Web"))
                mqWeb = new System.Messaging.MessageQueue(@".\Private$\Web");
            else
                mqWeb = MessageQueue.Create(@".\Private$\Web");

            
            
            //Q Creation
            if (MessageQueue.Exists(@".\Private$\Dist"))
                mqDist = new System.Messaging.MessageQueue(@".\Private$\Dist");
            else
                mqDist = MessageQueue.Create(@".\Private$\Dist");

            mqDist.Formatter = new XmlMessageFormatter(new Type[] { typeof(XmlDocument) });
            
        }
       
        /// <summary>
        /// Thread que fica a escuta no fila de mensagens
        /// </summary>
        public void Receiver()
        {
            
            ComunicacaoFila comFila = new ComunicacaoFila();

            while (true)
            {
                //Thread.Sleep(new TimeSpan(0,0,10));

                System.Messaging.Message mensagem;

                try
                {
                    mensagem = mqDist.Receive(new TimeSpan(0, 0, 3));


                    EncomendaWEBooks encomendaWEbooks = new EncomendaWEBooks((XmlDocument)mensagem.Body);

                    //Envia para o log o tipo de mensagem que recebeu
                    actualizaLog(encomendaWEbooks.TipoMensagem, true);
                    
                    if (encomendaWEbooks.TipoMensagem.ToLower() == "colocar")
                    {
                        WEBooksDistribuidorService servicoDistribuidor = new WEBooksDistribuidorService();
                        servicoDistribuidor.EncomendaWEBooks = encomendaWEbooks;

                        WaitCallback EncomendaCallBack = new WaitCallback(servicoDistribuidor.TempoVidaEncomenda);
                        ThreadPool.QueueUserWorkItem(EncomendaCallBack);

                        actualizaLog(encomendaWEbooks.IdEncomenda, false);
                        
                    }
                    else if (encomendaWEbooks.TipoMensagem.ToLower() == "cancelar")
                    {
                        WEBooksDistribuidorService servicoDistribuidor = new WEBooksDistribuidorService();
                        servicoDistribuidor.EncomendaWEBooks = encomendaWEbooks;

                        WaitCallback CancelaCallBack = new WaitCallback(servicoDistribuidor.CancelaEncomenda);
                        ThreadPool.QueueUserWorkItem(CancelaCallBack);
                    }
                }
                catch
                {
                    //nao faz nada, significa que nao existem mensagens na fila
                }
                
            }
        }

        private void LogMensagens_Load(object sender, EventArgs e)
        {
            //inicia a thread que vai ficar a escuta da fila do Distribuidor
            Thread thread = new Thread(Receiver);

            thread.Start();
        }

        public void actualizaLog(string mensagem, bool tipo) {

            if (tipo)
            {
                MsgBox.Text += DateTime.Now.TimeOfDay.ToString() + " - Recebida: " + mensagem + "\r\n";
            }
            else {
                MsgBox.Text += DateTime.Now.TimeOfDay.ToString() + " - Enviada: " + mensagem + "\r\n";
            }
        }

        public void Logger()
        {
            while (true)
            {
                if (escreveNoLog != null)
                {
                    MsgBox.Text += DateTime.Now.TimeOfDay.ToString() + " - " + escreveNoLog + "\r\n";
                }
            }
        }
    }
}

