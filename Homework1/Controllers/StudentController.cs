using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Homework1.Models;
using System.IO;
using Newtonsoft.Json;

namespace Homework1.Controllers
{
    [Route("api/v1/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        static string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string sFile = System.IO.Path.Combine(sCurrentDirectory, @".\Data\Students.json");
        static string sFilePath = Path.GetFullPath(sFile);
        private static string json = System.IO.File.ReadAllText(sFilePath);
        private static List<StudentDto> _list = JsonConvert.DeserializeObject<List<StudentDto>>(json);
        
        /// <summary>
        /// Verilen parametrelere uyan öğrenciler döndürür.
        /// Sortby parametresiyle de sıralama yapılabilir
        /// </summary>
        [HttpGet]
        public IActionResult Get([FromQuery] StudentFilter studentFilter)
        {
            ///studentfilter'daki null olmayan parametreleri karşılayan öğrencileri döndürür
            var query = _list.Where(x =>
                        x.Id.Equals(studentFilter.Id != 0 ? studentFilter.Id : x.Id) &&
                        x.Number.ToLower().Equals(studentFilter.Number != null ? studentFilter.Number.ToLower() : x.Number.ToLower()) &&
                        x.FirstName.ToLower().Equals(studentFilter.FirstName != null ? studentFilter.FirstName.ToLower() : x.FirstName.ToLower()) &&
                        x.LastName.ToLower().Equals(studentFilter.LastName != null ? studentFilter.LastName.ToLower() : x.LastName.ToLower()) &&
                        x.TcNo.Equals(studentFilter.TcNo != null ? studentFilter.TcNo.ToLower() : x.TcNo.ToLower()) &&
                        x.Gender.ToLower().Equals(studentFilter.Gender != null ? studentFilter.Gender.ToLower() : x.Gender.ToLower()) &&
                        x.Department.ToLower().Equals(studentFilter.Department != null ? studentFilter.Department.ToLower() : x.Department.ToLower())
                        ).OrderBy(y => y.Id);

            /// Filtrelenen liste id'ye göre sıralanır. Sortby null değilse sıralamaya yollanır.
            ////Döndürüklecek student listesini Sortby ile verilen parametrelere göre sıralar
            if (!studentFilter.IsNull() && studentFilter.Sortby != null)
            {   
                
                var sortParams = studentFilter.Sortby.Split(';');
                ///Sortby ile gelen stringin bölünüp, sıralanmak için fonksiyona yollanır
                query = OrderBy(query, sortParams[0], true);
                ///Ardından başka parametreler de varsa o parametrelere göre sıralanır
                for (var i = 1; i < sortParams.Length; i++)
                {
                    query = OrderBy(query, sortParams[i]);
                }
            }

            return Ok(query);
        }

        /// <summary>
        /// <summary>Bodyden aldığı bilgilerle yeni öğrenci oluşturuyor</summary>
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentDto student)
        {
            /// Otomatik id üretip kalan attibute'ları yeni öğrenciye atıyorum
            if (!student.IsNull() )
            {
                StudentDto NewStudent = new StudentDto();
                NewStudent.Id = _list.Max(x => x.Id) + 1;
                NewStudent.Number = student.Number;
                NewStudent.FirstName = student.FirstName;
                NewStudent.LastName = student.LastName;
                NewStudent.TcNo = student.TcNo;
                NewStudent.Gender = student.Gender;
                NewStudent.Department = student.Department;
                _list.Add(NewStudent);
                return Ok(NewStudent);
            }
            return BadRequest("Empty student");

          /* Kullanıcının id girmesini önlemek için yazdığım kod. Çünkü id'yi otomatik oluşturuyorum
           * Ama CreateStudentDto'da id olmadığı için çalıştıramadım.
           * if (student.Id != 0)
            {
                string error = "You can't pass id as a parameter";
                return BadRequest(error);
            }
            else if (!student.IsNull())
            {
                int id = _list.Max(x => x.Id) + 1;
                student.Id = id;
                _list.Add(student);
                return Ok(student);
            }
            return BadRequest("Empty object");*/

        }
        
        /// <summary>
        /// Body'den aldığı id'ye ait kaydı body'den aldığı diğer değerlerle değiştiriyor
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody] UpdateStundetDto UpdateStudent)
        {
            var ToUpdate = _list.FirstOrDefault(x => x.Id == UpdateStudent.Id);
            if (ToUpdate != null)
            {
                ToUpdate.Number.Equals(UpdateStudent.Number != null ? ToUpdate.Number : UpdateStudent.Number);
                ToUpdate.FirstName.Equals(UpdateStudent.FirstName != null ? ToUpdate.FirstName : UpdateStudent.FirstName);
                ToUpdate.LastName.Equals(UpdateStudent.LastName != null ? ToUpdate.LastName : UpdateStudent.LastName);
                ToUpdate.TcNo.Equals(UpdateStudent.TcNo != null ? ToUpdate.TcNo : UpdateStudent.TcNo);
                ToUpdate.Gender.Equals(UpdateStudent.Gender != null ? ToUpdate.Gender : UpdateStudent.Gender);
                ToUpdate.Department.Equals(UpdateStudent.Department != null ? ToUpdate.Department : UpdateStudent.Department);
                return Ok(ToUpdate);
            }
            string error = $"There is no record with id = {UpdateStudent.Id}";
            return NotFound(error);
        }

        /// <summary>
        /// Query'den gelen id'ye ait kayıdı siliyor
        /// </summary>
        [HttpDelete]
        public IActionResult Delete([FromQuery] DeleteStudentDto student)
        {
            var ToDelete = _list.FirstOrDefault(x => x.Id == student.Id);
            if (ToDelete != null)
            {
                _list.Remove(ToDelete);
                return Ok(_list);
            }
            return Ok();
        }

        /// <summary>
        /// Get fonksiyonunda studentlerın sıralanmasını sağlayan fonksiyon
        /// </summary>
        /// <param name="query"> Filtreli halde gelen liste</param>
        /// <param name="param"> Listenin neye göre sıralanacağı</param>
        /// <param name="first"> Listenin daha önce sıralanıp sıralanmadı</param>
        /// <returns></returns>
        public IOrderedEnumerable<StudentDto> OrderBy(IOrderedEnumerable<StudentDto> query, string param, bool first = false) 
        {
            var mi = param.Split(',');
            if (mi.Length >= 2)
            {
                if (mi[1] == "desc")
                {
                    switch (mi[0])
                    {
                        case "id": query = first ? query.OrderByDescending(o => o.Id) : query.ThenByDescending(o => o.Id); break;
                        case "number": query = first ? query.OrderByDescending(o => o.Number) : query.ThenByDescending(o => o.Number); break;
                        case "firstname": query = first ? query.OrderByDescending(o => o.FirstName) : query.ThenByDescending(o => o.FirstName); break;
                        case "lastname": query = first ? query.OrderByDescending(o => o.LastName) : query.ThenByDescending(o => o.LastName); break;
                        case "tcno": query = first ? query.OrderByDescending(o => o.TcNo) : query.ThenByDescending(o => o.TcNo); break;
                        case "gender": query = first ? query.OrderByDescending(o => o.Gender) : query.ThenByDescending(o => o.Gender); break;
                        case "department": query = first ? query.OrderByDescending(o => o.Department) : query.ThenByDescending(o => o.Department); break;
                        default: break;
                    }
                    return query;
                }
            }

            switch (mi[0])
            {
                case "id": query = first ? query.OrderBy(o => o.Id) : query.ThenBy(o => o.Id); break;
                case "number": query = first ? query.OrderBy(o => o.Number) : query.ThenBy(o => o.Number); break;
                case "firstname": query = first ? query.OrderBy(o => o.FirstName) : query.ThenBy(o => o.FirstName); break;
                case "lastname": query = first ? query.OrderBy(o => o.LastName) : query.ThenBy(o => o.LastName); break;
                case "tcno": query = first ? query.OrderBy(o => o.TcNo) : query.ThenBy(o => o.TcNo); break;
                case "gender": query = first ? query.OrderBy(o => o.Gender) : query.ThenBy(o => o.Gender); break;
                case "department": query = first ? query.OrderBy(o => o.Department) : query.ThenBy(o => o.Department); break;
                default: break;
            }

            return query;
        }

    }
}
