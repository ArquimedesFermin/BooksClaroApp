using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BooksClaroApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace BooksClaroApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        
        /// <summary>
        /// Obtener listado de libros
        /// </summary>
        /// <returns>Lista de libros</returns>
        
        [HttpGet]
        [Route("GetBooks")]
        public async Task<IEnumerable<Books>> Get()
        {
            var books = new List<Books>();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");

            var url = "/api/Books";
            HttpResponseMessage respuesta = await client.GetAsync(url);


            if (respuesta.IsSuccessStatusCode)
            {
                var resp = await respuesta.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<List<Books>>(resp);
            }
            return books;

        }

        /// <summary>
        /// Obtener libro por el id de identificación
        /// </summary>
        /// <param name="id">El id del libro seleccionado</param>
        /// <returns>La información del libro seleccionado</returns>

        [HttpGet]
        [Route("GetBooks/{id}")]

        public async Task<Books> Get(int id)
        {
            var books = new Books();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");

            var url = "/api/Books/" + id + "";
            HttpResponseMessage respuesta = await client.GetAsync(url);


            if (respuesta.IsSuccessStatusCode)
            {
                var resp = await respuesta.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<Books>(resp);
            }

            return books;

        }

        /// <summary>
        /// Metodo para el registro de los libro
        /// </summary>
        /// <param name="books">Objeto de tipo books</param>
        /// <returns>Http code</returns>
        
        [HttpPost]
        [Route("PostBooks")]
        public async Task<HttpStatusCode> Post([FromBody] Books books)
        {

            var status = new HttpStatusCode();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");


            var booksJson = JsonConvert.SerializeObject(books);
            var data = new StringContent(booksJson, Encoding.UTF8, "application/json");

            var url = "/api/Books/";
            HttpResponseMessage respuesta = await client.PostAsync(url, data);

            if (respuesta.IsSuccessStatusCode)
            {
                _ = respuesta.Content.ReadAsStringAsync();
                await Task.CompletedTask;
                status = respuesta.StatusCode;
            }
            else
            {
                status = respuesta.StatusCode;
            }

            return status;

        }

        /// <summary>
        /// Metodo para la modificación de los registro de los libros
        /// </summary>
        /// <param name="id">El id del libro seleccionado</param>
        /// <param name="books">Objeto de tipo books</param>
        /// <returns>Http code</returns>

        [HttpPut]
        [Route("UpdateBooks/{id}")]
        public async Task<HttpStatusCode> Put(int id, [FromBody] Books booksObject)
        {
            var books = new Books();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");

            //Get
            var urlGet = "/api/Books/" + id + "";
            HttpResponseMessage respuestaGet = await client.GetAsync(urlGet);


            if (respuestaGet.IsSuccessStatusCode)
            {
                var resp = await respuestaGet.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<Books>(resp);
            }


            books.ID = booksObject.ID;
            books.Title = booksObject.Title;
            books.Description = booksObject.Description;
            books.PageCount = booksObject.PageCount;
            books.Excerpt = booksObject.Excerpt;
            books.PublishDate = booksObject.PublishDate;



            //Update
            var booksJson = JsonConvert.SerializeObject(books);
            var data = new StringContent(booksJson, Encoding.UTF8, "application/json");

            var urlUpdate = "/api/Books/" + id + "";
            HttpResponseMessage respuestaUpdate = await client.PutAsync(urlUpdate, data);

            HttpStatusCode status;
            if (respuestaUpdate.IsSuccessStatusCode)
            {
                _ = respuestaUpdate.Content.ReadAsStringAsync();
                await Task.CompletedTask;
                status = respuestaUpdate.StatusCode;
            }
            else
            {
                status = respuestaUpdate.StatusCode;

            }
            return status;
        }

        /// <summary>
        /// Metodo para eliminar el registro de lo libros
        /// </summary>
        /// <param name="id">El id del libro seleccionado</param>
        /// <returns>Http code</returns>

        [HttpDelete]
        [Route("DeleteBooks/{id}")]
        public async Task<HttpStatusCode> Delete(int id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");

            var url = "/api/Books/" + id + "";
            HttpResponseMessage respuesta = await client.DeleteAsync(url);


            HttpStatusCode status;
            if (respuesta.IsSuccessStatusCode)
            {
                _ = respuesta.Content.ReadAsStringAsync();
                await Task.CompletedTask;
                status = respuesta.StatusCode;
            }
            else
            {
                status = respuesta.StatusCode;

            }
            return status;

        }
    }
}
