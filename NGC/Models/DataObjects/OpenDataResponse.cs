using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NGC.Models.DataObjects
{
    public class OpenDataResponse
    {
        public class Parameters
        {

            [JsonProperty("dataset")]
            public IList<string> dataset { get; set; }

            [JsonProperty("timezone")]
            public string timezone { get; set; }

            [JsonProperty("q")]
            public string q { get; set; }

            [JsonProperty("rows")]
            public int rows { get; set; }

            [JsonProperty("format")]
            public string format { get; set; }
        }

        public class Fields
        {
           

            [JsonProperty("efetcent")]
            public string efetcent { get; set; }

            [JsonProperty("l6_declaree")]
            public string l6_declaree { get; set; }

            [JsonProperty("l6_normalisee")]
            public string l6_normalisee { get; set; }

            [JsonProperty("libtefen")]
            public string libtefen { get; set; }

            [JsonProperty("iriset")]
            public string iriset { get; set; }

            [JsonProperty("libcom")]
            public string libcom { get; set; }

            [JsonProperty("typvoie")]
            public string typvoie { get; set; }

            [JsonProperty("dapet")]
            public string dapet { get; set; }

            [JsonProperty("dcren")]
            public string dcren { get; set; }

            [JsonProperty("l1_normalisee")]
            public string l1_normalisee { get; set; }

            [JsonProperty("epci")]
            public string epci { get; set; }

            [JsonProperty("ddebact")]
            public string ddebact { get; set; }

            [JsonProperty("categorie")]
            public string categorie { get; set; }

            [JsonProperty("tcd")]
            public int tcd { get; set; }

            [JsonProperty("modet")]
            public string modet { get; set; }

            [JsonProperty("libtefet")]
            public string libtefet { get; set; }

            [JsonProperty("proden")]
            public string proden { get; set; }

            [JsonProperty("libtu")]
            public string libtu { get; set; }

            [JsonProperty("siren")]
            public string siren { get; set; }

            [JsonProperty("nom_dept")]
            public string nom_dept { get; set; }

            [JsonProperty("apet700")]
            public string apet700 { get; set; }

            [JsonProperty("depcomet")]
            public string depcomet { get; set; }

            [JsonProperty("section")]
            public string section { get; set; }

            [JsonProperty("tu")]
            public string tu { get; set; }

            [JsonProperty("lieuact")]
            public string lieuact { get; set; }

            [JsonProperty("libvoie")]
            public string libvoie { get; set; }

            [JsonProperty("defen")]
            public string defen { get; set; }

            [JsonProperty("libapet")]
            public string libapet { get; set; }

            [JsonProperty("depcomen")]
            public string depcomen { get; set; }

            [JsonProperty("amintret")]
            public string amintret { get; set; }

            [JsonProperty("code_section")]
            public string code_section { get; set; }

            [JsonProperty("siret")]
            public string siret { get; set; }

            [JsonProperty("l4_normalisee")]
            public string l4_normalisee { get; set; }

            [JsonProperty("prodet")]
            public string prodet { get; set; }

            [JsonProperty("libactivnat")]
            public string libactivnat { get; set; }

            [JsonProperty("codpos")]
            public string codpos { get; set; }

            [JsonProperty("l1_declaree")]
            public string l1_declaree { get; set; }

            [JsonProperty("code_classe")]
            public string code_classe { get; set; }

            [JsonProperty("l4_declaree")]
            public string l4_declaree { get; set; }

            [JsonProperty("amintren")]
            public string amintren { get; set; }

            [JsonProperty("apen700")]
            public string apen700 { get; set; }

            [JsonProperty("siege")]
            public string siege { get; set; }

            [JsonProperty("nic")]
            public string nic { get; set; }

            [JsonProperty("tefet")]
            public string tefet { get; set; }

            [JsonProperty("libreg_new")]
            public string libreg_new { get; set; }

            [JsonProperty("nj")]
            public string nj { get; set; }

            [JsonProperty("libmoden")]
            public string libmoden { get; set; }

            [JsonProperty("numvoie")]
            public string numvoie { get; set; }

            [JsonProperty("nicsiege")]
            public string nicsiege { get; set; }

            [JsonProperty("libmodet")]
            public string libmodet { get; set; }

            [JsonProperty("ctonet")]
            public string ctonet { get; set; }

            [JsonProperty("diffcom")]
            public string diffcom { get; set; }

            [JsonProperty("dcret")]
            public string dcret { get; set; }

            [JsonProperty("du")]
            public string du { get; set; }

            [JsonProperty("coordonnees")]
            public IList<double> coordonnees { get; set; }

            [JsonProperty("depet")]
            public string depet { get; set; }

            [JsonProperty("code_division")]
            public string code_division { get; set; }

            [JsonProperty("code_groupe")]
            public string code_groupe { get; set; }

            [JsonProperty("datemaj")]
            public DateTime datemaj { get; set; }

            [JsonProperty("nomen_long")]
            public string nomen_long { get; set; }

            [JsonProperty("l7_normalisee")]
            public string l7_normalisee { get; set; }

            [JsonProperty("defet")]
            public string defet { get; set; }

            [JsonProperty("activnat")]
            public string activnat { get; set; }

            [JsonProperty("dapen")]
            public string dapen { get; set; }

            [JsonProperty("activite")]
            public string activite { get; set; }

            [JsonProperty("libnj")]
            public string libnj { get; set; }

            [JsonProperty("tefen")]
            public string tefen { get; set; }

            [JsonProperty("efencent")]
            public string efencent { get; set; }

            [JsonProperty("rpen")]
            public string rpen { get; set; }

            [JsonProperty("origine")]
            public string origine { get; set; }

            [JsonProperty("libapen")]
            public string libapen { get; set; }

            [JsonProperty("zemet")]
            public string zemet { get; set; }

            [JsonProperty("arronet")]
            public string arronet { get; set; }

            [JsonProperty("saisonat")]
            public string saisonat { get; set; }

            [JsonProperty("rpet")]
            public string rpet { get; set; }

            [JsonProperty("sous_classe")]
            public string sous_classe { get; set; }

            [JsonProperty("moden")]
            public string moden { get; set; }

            [JsonProperty("comet")]
            public string comet { get; set; }

            [JsonProperty("auxilt")]
            public string auxilt { get; set; }

            [JsonProperty("uu")]
            public string uu { get; set; }

            [JsonProperty("ind_publipo")]
            public string ind_publipo { get; set; }

            [JsonProperty("liborigine")]
            public string liborigine { get; set; }

            [JsonProperty("l3_declaree")]
            public string l3_declaree { get; set; }

            [JsonProperty("prenom")]
            public string prenom { get; set; }

            [JsonProperty("monoact")]
            public string monoact { get; set; }

            [JsonProperty("libmonoact")]
            public string libmonoact { get; set; }

            [JsonProperty("natetab")]
            public string natetab { get; set; }

            [JsonProperty("nom")]
            public string nom { get; set; }

            [JsonProperty("civilite")]
            public string civilite { get; set; }

            [JsonProperty("libnatetab")]
            public string libnatetab { get; set; }

            [JsonProperty("l2_normalisee")]
            public string l2_normalisee { get; set; }

            [JsonProperty("sigle")]
            public string sigle { get; set; }

            [JsonProperty("tca")]
            public string tca { get; set; }

            [JsonProperty("esaapen")]
            public string esaapen { get; set; }

            [JsonProperty("esaann")]
            public string esaann { get; set; }

            [JsonProperty("l3_normalisee")]
            public string l3_normalisee { get; set; }

            [JsonProperty("esasec1n")]
            public string esasec1n { get; set; }

            [JsonProperty("l2_declaree")]
            public string l2_declaree { get; set; }

            [JsonProperty("libesaapen")]
            public string libesaapen { get; set; }

            [JsonProperty("libtca")]
            public string libtca { get; set; }

            [JsonProperty("actisurf")]
            public string actisurf { get; set; }

            [JsonProperty("libactisurf")]
            public string libactisurf { get; set; }
        }

        public class Geometry
        {

            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("coordinates")]
            public IList<double> coordinates { get; set; }
        }

        public class Record
        {

            [JsonProperty("datasetid")]
            public string datasetid { get; set; }

            [JsonProperty("recordid")]
            public string recordid { get; set; }

            [JsonProperty("fields")]
            public Fields fields { get; set; }

            [JsonIgnore]
            public string Name { get { return fields.nomen_long; } }

            [JsonProperty("geometry")]
            public Geometry geometry { get; set; }

            [JsonProperty("record_timestamp")]
            public DateTime record_timestamp { get; set; }
        }

        public class Example
        {

            [JsonProperty("nhits")]
            public int nhits { get; set; }

            [JsonProperty("parameters")]
            public Parameters parameters { get; set; }

            [JsonProperty("records")]
            public IList<Record> records { get; set; }
        }
    }
}
