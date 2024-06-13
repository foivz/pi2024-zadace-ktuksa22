using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treca_Zadaca.Modeli
{ 
public class Osoba
{
    public int Id { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string KorisnickoIme { get; set; }
    public int Lozinka { get; set; }
    public int IdNalaza { get; set; }


    public NalaziKorisnika NalaziKorisnika { get; set; }
    }
}
