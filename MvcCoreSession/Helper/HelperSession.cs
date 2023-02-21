using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace MvcCoreSession.Helper
{
    public static class HelperSession
    {
        //NECESITAMOS UN METODO PARA GUARDAR DATOS EN SESSION
        //Y DEBEMOS EXTENDER LA CLASE ISession
        //PARA GUARDAR UN OBJETO NECESITAMOS SU KEY Y EL PROPIO OBJETO A GUARDAR
        public static void SetObject(this ISession session, string key, object value)
        {
            //GUARDAMOS LA INFORMACIÓN DEL OBJETO CON FORMATO JSON
            string data = JsonConvert.SerializeObject(value);
            //ALMACENAMOS EL TEXTO EN SESSION
            session.SetString(key, data);
        }

        //NECESITAMOS OTRO METODO PARA RECUPERAR CUALQUIER OBJETO DE SESSION
        //DEBEN INDICARNOS EL TIPO DE OBJETO A RECUPERAR MEDIANTE <T>. Y LA CLAVE A RECUPERAR
        public static T GetObject<T>(this ISession session, string key)
        {
            //RECUPERAMOS EL JSON QUE HAY EN LA SESSION
            string data = session.GetString(key);
            //SI NO EXISTE LA CLAVE, DEVOLVEMOS NULL
            if (data == null)
            {
                return default(T);
            }
            else
            {
                T objeto = JsonConvert.DeserializeObject<T>(data);
                return objeto;
            }
        }
    }
}
