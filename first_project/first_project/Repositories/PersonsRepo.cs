using System;
using System.Data;
using Dapper;
using first_project.Models;
using first_project.Models.Data;

namespace first_project.Repositories
{
    public class PersonsRepo : IPersonsRepo


    {
        private readonly DapperDBContext context;
        public PersonsRepo(DapperDBContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Create person
        /// </summary>
        /// <param name="personsModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> CreatePerson(PersonsModel personsModel)
        {
            string response = string.Empty;
            string query = "Insert into Persons(Id,Name,Age,Address,Phone) values (@Id,@Name,@Age,@Address,@Phone)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", personsModel.Id, DbType.Int16);
            parameters.Add("Name", personsModel.Name, DbType.String);
            parameters.Add("Age", personsModel.Age, DbType.Int16);
            parameters.Add("Address", personsModel.Address, DbType.String);
            parameters.Add("Phone", personsModel.Phone, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }
        /// <summary>
        /// Delete person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public  async Task<string> DeletePerson(int id)
        {


            string response = string.Empty;
            var query = "DELETE FROM Persons where Id=@Id";
            using (var connection = this.context.CreateConnection())
            {
                var persons = await connection.ExecuteAsync(query, new { id });
                response = "pass";
            }
            return response;

        }
        /// <summary>
        /// Get All Persons
        /// </summary>
        /// <returns></returns>

        public async Task<List<PersonsModel>> GetAllPersons()
        {
            var query = "SELECT*FROM Persons";
            using (var connection = this.context.CreateConnection())
            {
                var persons = await connection.QueryAsync<PersonsModel>(query);
                return persons.ToList();
            }

        }
        /// <summary>
        /// Get person by his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public  async Task<PersonsModel> GetPersonsbyID(int id)
        {

            var query = "SELECT*FROM Persons where Id=@Id";
            using (var connection = this.context.CreateConnection())
            {
                var persons = await connection.QueryFirstOrDefaultAsync<PersonsModel>(query,new {id});
                return persons;
            }
        }
        /// <summary>
        /// Update person
        /// </summary>
        /// <param name="personsModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> UpdatePerson(PersonsModel personsModel, int id)
        {
            string response = string.Empty;
            string query = "update Persons set Name=@Name,Age=@Age,Address=@Address,Phone=@Phone where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", personsModel.Id, DbType.Int16);
            parameters.Add("Name", personsModel.Name, DbType.String);
            parameters.Add("Age", personsModel.Age, DbType.Int16);
            parameters.Add("Address", personsModel.Address, DbType.String);
            parameters.Add("Phone", personsModel.Phone, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }
    }
}

