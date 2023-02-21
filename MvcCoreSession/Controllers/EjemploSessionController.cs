using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Helper;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        private int numero = 1;

        public IActionResult EjemploSimple()
        {
            ViewData["NUMERO"] = this.numero;
            return View();
        }
        [HttpPost]
        public IActionResult EjemploSimple(string accion, string usuario)
        {
            //PREGUNTAMOS SI QUEREMOS GUARDAR DATOS DE LA SESSION
            if (accion.ToLower() == "almacenar")
            {
                this.numero += 1;
                //GUARDAMOS LOS DATOS EN SESSION
                HttpContext.Session.SetString("NUMERO", numero.ToString());
                HttpContext.Session.SetString("USUARIO", usuario);
                HttpContext.Session.SetString("HORA", DateTime.Now.ToLongTimeString());
                ViewData["MENSAJE"] = "Datos almacenados en Session";
            }else if (accion.ToLower() == "mostrar")
            {
                //RECUPERAMOS LOS DATOS DE SESSSION
                ViewData["NUMERO"] = HttpContext.Session.GetString("NUMERO");
                ViewData["USUARIO"] = HttpContext.Session.GetString("USUARIO");
                ViewData["HORA"] = HttpContext.Session.GetString("HORA");
            }


            return View();
        }
        public IActionResult SessionPersonas()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SessionPersonas(Persona persona)
        {
            //CUANDO PULSE UN BOTON GUARDAMOS UNA PERSONA
            HttpContext.Session.SetObject("PERSONA", persona);
            ViewData["MENSAJE"] = "Persona almacenada en Session";
            //SI LO QUE DESEAMOS ES ALMACENAR UN COJUNTO DE PERSONA LO PRIMERO QUE NECESITAMOS
            //ES COMPROBAR ES SI YA EXISTE ALGUNA PERSONA ALMACENADA EN SESSION
            List<Persona> personas =
                HttpContext.Session.GetObject<List<Persona>>("LISTAPERSONAS");
            //COMPROBAMOS SI TENEMOS PERSSONAS ALMACENADAS
            if (personas == null)
            {
                //TODAVIA NO HAY NINGUNA PERSONA ALMACENADA
                //CREAMOS COLECCION
                personas = new List<Persona>();
            }
            //AÑADIMOS UNA NUEVA PERSONA A LA COLECCIÓN
            personas.Add(persona);
            //ALMACENAMOS LAS PERSONAS EN SESSION
            HttpContext.Session.SetObject("LISTAPERSONAS", personas);
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string userName)
        {
            HttpContext.Session.SetString("USERNAME", userName);
            ViewData["MENSAJE"] = "Usuario logeado";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USERNAME");
            return RedirectToAction("Index", "Home");
        }
    }
}
