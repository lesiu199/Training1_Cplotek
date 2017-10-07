using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zarzadzanie_pojazdami_s1
{
    class Program
    {
        static void Main(string[] args)
        {
             Ladowe Opancerzony_woz_bojowy = new Ladowe();
             Ladowe Czolg = new Ladowe();
             Ladowe projektAmfibii = new Ladowe();
             IPojWodnoLadowe Amfibia = projektAmfibii; // problem z amfibia ???
             Wodne Niszczyciel = new Wodne();
             Powietrzne Puszczyk = new Powietrzne();
             Ladowe Ambulans = new Ladowe();
             Wodne Zaglowka = new Wodne();

            Opancerzony_woz_bojowy.AtributeSet("Opancerzony woz bojowy", 8, 3, 20, 120);
            Czolg.AtributeSet("Czolg", 0, 5, 40, 80);
            projektAmfibii.AtributeSet("Amfibia", 4, 2, 20, 60);
            Niszczyciel.AtributeSet("Niszczyciel", 0, 50, 90, 40);
            Puszczyk.AtributeSet("Puszczyk", 0, 2, 10, 130);
            Ambulans.AtributeSet("Karetka", 4, 4, 0, 150);
            Zaglowka.AtributeSet("Zaglowka", 0, 6, 0, 30);

            

            /*
            -opancerzony wóz bojowy: nazwa kodowa, ilość kol, liczba załogantów, kaliber zamontowanego działa.
            -czołg: nazwa kodowa, liczba załogantów, kaliber zamontowanego działa, maksymalna prędkość.
            -amfibia: nazwa kodowa, liczba kol, liczba załogantów, kaliber zamontowanego działa, maksymalna prędkość morska, maksymalna prędkość lądowa.
            -niszczyciel: nazwa kodowa, liczba załogantów, kaliber zamontowanego działa, maksymalna prędkość morska.
            -helikopter bojowy puszczyk: nazwa kodowa, liczba załogantów, kaliber zamontowanego działa, maksymalna prędkość powietrzna.
            -ambulans: nazwa kodowa, ilość kol, liczba załogantów.
            -zagłówka: nazwa kodowa, liczba załogantów, maksymalna prędkość morska
           
            */
            Woz[] wozyTab = new Woz[] { Opancerzony_woz_bojowy,Ambulans};
            foreach (Woz k in wozyTab)
            {
                k.Meldunek();
                k.Ognia();
            }
            Ladowe[] wozyLadowe = new Ladowe[] { Opancerzony_woz_bojowy,(Ladowe) Amfibia, Czolg, Ambulans };
            foreach(Ladowe k in wozyLadowe)
            {
                k.Jedz();
                k.Ognia();
                k.Meldunek();
            }
            Wodne[] wozyWodne = new Wodne[] { Zaglowka, Niszczyciel, (Wodne)Amfibia };
            foreach (Wodne k in wozyWodne)
            {
                k.Plyn();
                k.Ognia();
                k.Meldunek();
            }
            Console.ReadLine();
        }

    }

    #region klasa glowna 

    abstract class Woz
    {
        #region Pola - woz
        private string nazwa_kodowa;
        private int liczba_kol;
        private int liczba_zalogantow;
        private double kaliber; // w mm 
        private int maks_wodne_V; // km/h
        private int maks_pow_V; // km/h
        private int maks_ladowe_V; // km/h
        #endregion

        #region Woz - wlasciwosci
        public string Nazwa_kodowa
        {
            set
            {
                nazwa_kodowa = value;
            }
        }
        public int Liczba_kol
        {
            set
            {
                liczba_kol = value;
            }
        }
        public int Liczba_zalogantow
        {
            set
            {
                liczba_zalogantow = value;
            }
        }
        public double Kaliber
        {
            set
            {
                kaliber = value;
            }
        }
        public int Maks_pow_V
        {
            get
            {
                return maks_pow_V;
            }
            set
            {
                maks_pow_V = value;
            }
        }
        public int Maks_wodne_V
        {
            get
            {
                return maks_wodne_V;
            }
            set
            {
                maks_wodne_V = value;
            }
        }
        public int Maks_ladowe_V
        {
            get
            {
                return maks_ladowe_V;
            }
            set
            {
                maks_ladowe_V = value;
            }
        }
        #endregion

        #region Metody - woz
        public Woz()
        {
            this.nazwa_kodowa = "default";
            this.liczba_kol = 0;
            this.liczba_zalogantow = 0;
            this.kaliber = 0; // w mm 
        }

        public virtual void Meldunek()
        {
            Console.WriteLine("Melduje sie: \n Pojazd o kodzie: {0} z iloscia kol: {1} oraz zaloga {2} osobowa.\n" +
                               " Posiadamy dzialo o kaliberze {3} mm.\n Koniec meldunku.\n",
                               nazwa_kodowa,liczba_kol,liczba_zalogantow,kaliber);
        }

        public bool Ognia()
        {
            if (kaliber == 0.0)
            {
                Console.WriteLine("Panie nie ma czym...\n");
                return false;
            } 
            else
            {
                Console.WriteLine("Przyjalem. Cel. Pal. KABUUMMMMM.... !!!!");
                return true;
            }
        }
        #endregion
    }
    #endregion
    #region Woz - Powietrzne

    class Powietrzne : Woz, ILatanie
    {

        public Powietrzne() : base()
        {

        }

        public void AtributeSet(string kod, int kola, int zaloga, double kali, int V)
        {
            Nazwa_kodowa = kod;
            Liczba_kol = kola;
            Liczba_zalogantow = zaloga;
            Kaliber = kali;
            Maks_pow_V = V;
        }

        public override void Meldunek()
        {
            base.Meldunek();
            Console.WriteLine("Zaznaczyc jeszcze trzeba ze poruszamy sie z predkoscia powietrzna {0} km/h.\n",Maks_pow_V );
        }
        public void Lataj()
        {
            Console.WriteLine("latamy panie kapitanie");
        }

    }

    #endregion

    #region Woz - Ladowe
    class Ladowe : Woz, IJezdzenie, IPojWodnoLadowe
    {

        public Ladowe() : base()
        {
        
        }
        public void AtributeSet(string kod, int kola, int zaloga, double kali, int V)
        {
            Nazwa_kodowa = kod;
            Liczba_kol = kola;
            Liczba_zalogantow = zaloga;
            Kaliber = kali;
            Maks_ladowe_V = V;
        }
        public override void Meldunek()
        {
            base.Meldunek();
            Console.WriteLine("Zaznaczyc jeszcze trzeba ze poruszamy sie z predkoscia ladowa {0} km/h.\n", Maks_ladowe_V);
        }

        public void Jedz()
        {
            Console.WriteLine("jedziemy panie kapitanie");
        }
        void IPojWodnoLadowe.Plyn()
        {
            Console.WriteLine("plyniemy panie kapitanie");
        }
    }
    #endregion

    #region Woz - Wodne
    class Wodne : Woz, IPlywanie, IPojWodnoLadowe
    {
        public Wodne() : base()
        {
           
        }
        public void AtributeSet(string kod, int kola, int zaloga, double kali, int V)
        {
            Nazwa_kodowa = kod;
            Liczba_kol = kola;
            Liczba_zalogantow = zaloga;
            Kaliber = kali;
            Maks_wodne_V = V;
        }
        public override void Meldunek()
        {
            base.Meldunek();
            Console.WriteLine("Zaznaczyc jeszcze trzeba ze poruszamy sie z predkoscia wodna {0} km/h.\n", Maks_wodne_V);
        }
        public void Plyn()
        {
            Console.WriteLine("Plyniemy panie kapitanie.");
        }
        void IPojWodnoLadowe.Jedz()
        {
            Console.WriteLine("jedziemy panie kapitanie");
        }
    }
    #endregion

    #region inrefacey
    interface IJezdzenie
    {
        void Jedz();
    }

    interface IPlywanie
    {
        void Plyn();
    }
    interface ILatanie
    {
        void Lataj();
    }

    interface IPojWodnoLadowe
    {
        void Plyn();

        void Jedz();
    }

    #endregion
}
