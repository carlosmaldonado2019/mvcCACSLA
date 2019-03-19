using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcCACSLA.Models.Constantes
{
    public  class EstandarNum
    {
        public const int comp_ = 1;
        public const int estu_ = 2;
        public const int eval_ = 3;
        public const int plan_ = 4;
        public const int form_ = 5;
        public const int serv_ = 6;
        public const int vinc_ = 7;
        public const int inve_ = 8;
        public const int infr_ = 9;
        public const int gest_ = 10;
        public const int impa_ = 11;
        public const int emej_ = 12;

        public const string Scomp_ = "Competencia del Profesorado";
        public const string Sestu_ = "Estudiantes";
        public const string Seval_ = "Evaluación de la Enseñanza - Aprendizaje";
        public const string Splan_ = "Plan de Estudios";
        public const string Sform_ = "Formación Integral";
        public const string Sserv_ = "Servicios Institucionales de Apoyo";
        public const string Svinc_ = "Vinculación - Extensión";
        public const string Sinve_ = "Investigación";
        public const string Sinfr_ = "Infraestructura";
        public const string Sgest_ = "Gestión Administrativa";
        public const string Simpa_ = "Impacto Social";
        public const string Semej_ = "Evaluación y Mejora";

        public const string RoleAdmin = "Administrador";
        public const string RoleUser = "Usuario";
        public const string RoleCoord = "Coordinador";
        public const string RoleAcree = "Acreeditador";

        //Idcoordinacion para que se muestren todas 
        public const int _Idcoord = 35;
        public const bool SubeCarreas = true;
        public const bool NoSubeCarreas = false;


    }
}