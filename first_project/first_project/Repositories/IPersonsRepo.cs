using System;
using first_project.Models;

namespace first_project.Repositories
{
	public interface IPersonsRepo
	{
		Task<List<PersonsModel>> GetAllPersons();

        Task<PersonsModel> GetPersonsbyID(int id);

        Task<string> CreatePerson(PersonsModel personsModel);
        Task<string> UpdatePerson(PersonsModel personsModel,int id);
        Task<string> DeletePerson(int id);




    }
}

