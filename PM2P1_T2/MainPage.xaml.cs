using PM2P1_T2.Model;
using PM2P1_T2.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PM2P1_T2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Nombres.Text) ||
                string.IsNullOrEmpty(Apellidos.Text) ||
                string.IsNullOrEmpty(Edad.Text) ||
                string.IsNullOrEmpty(Correo.Text))
            {
                mensaje("Aviso", "Debe llenar todos los campos");
                return;
            }

            try
            {
                if (int.Parse(Edad.Text) < 0)
                {
                    mensaje("Aviso", "La edad debe se mayor a cero");
                    return;
                }

            }
            catch (Exception ex) { 
                mensaje("Aviso", ex.Message);
                return;
            }

            if (!ValidateEmail(Correo.Text))
            {
                mensaje("Aviso", "Correo no valido");
                return;
            }

            Persona persona = new Persona() {
                nombres = Nombres.Text,
                apellidos = Apellidos.Text,
                edad = Edad.Text,
                correo = Correo.Text
            };

            SecundaryPage secundary = new SecundaryPage();
            secundary.BindingContext = persona;

            await Navigation.PushAsync(secundary);
        }

        public bool ValidateEmail(string email)
        {
            Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }

        public async void mensaje(string titulo, string cuerpo)
        {
            await DisplayAlert(titulo, cuerpo, "OK");
        }

    }
}
