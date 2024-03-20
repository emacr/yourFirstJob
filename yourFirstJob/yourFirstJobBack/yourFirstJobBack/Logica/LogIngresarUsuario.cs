using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobBack.Logica
{
    public class LogIngresarUsuario
    {
        public ResIngresarPublicacion ingresarPublicacion(ReqIngresarPublicacion req)
        {
            ResIngresarPublicacion res = new ResIngresarPublicacion();

            try
            {
                //Conectar

                res.resultado = false;
                res.listaDeErrores = new List<String>();
                if (req == null)
                {
                    res.resultado = false;
                    res.listaDeErrores.Add("Request nulo");
                }
                else
                {
                    if (req.publicacion.idUsuario == 0)
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("No se recibio el usuario");
                    }
                    if (String.IsNullOrEmpty(req.publicacion.titulo))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Titulo faltante");
                    }
                    if (String.IsNullOrEmpty(req.publicacion.mensaje))
                    {
                        res.resultado = false;
                        res.listaDeErrores.Add("Mensaje faltante");
                    }
                }
                //BD
                if (res.listaDeErrores.Any())
                {
                    //Hubo al menos 1 error
                    res.resultado = false;

                }
                else
                {
                    //Llamar base de datos
                    ConexionLinqDataContext conexion = new ConexionLinqDataContext();
                    int? idReturn = 0;
                    int? errorId = 0;
                    string errorDescripcion = "";

                    conexion.SP_INGRESAR_PUBLICACION(req.publicacion.idTema, req.publicacion.idUsuario, req.publicacion.titulo, req.publicacion.mensaje, ref idReturn, ref errorId, ref errorDescripcion);
                    if (idReturn == 0)
                    {
                        //Error en base de datos
                        //No se hizo la publicacion
                        res.resultado = false;
                        res.listaDeErrores.Add(errorDescripcion);
                    }
                    else
                    {
                        res.resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //Si falla
                res.resultado = false;
                res.listaDeErrores.Add(ex.ToString());

            }
            finally
            {
                //Bitacora
            }


            return res;
        }


    }
}
