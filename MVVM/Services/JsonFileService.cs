using MVVM.Models;
using MVVM.Readers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Services
{
    /// <summary>
    /// JSON file service.
    /// </summary>
    public class JsonFileService : IFileService
    {
        private readonly IFileReader _fileReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFileService"/> class.
        /// </summary>
        /// <param name="fileReader">File reader.</param>
        /// <exception cref="ArgumentNullException">File reader is null.</exception>
        public JsonFileService(IFileReader fileReader)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        /// <summary>
        /// Get all Currency statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{Currency}}".</returns>
        /*public async Task<IList<Currency>> GetCurrencyStatistic()
        {
            var allStatistic = await _fileReader.ReadDirectory();
            return GetUsers(allStatistic);
        }*/

        /// <summary>
        /// Get all Currency statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{Currency}}".</returns>
        /*public async Task<IList<Currency>> GetCurrencyStatistic(string[] files)
        {
            var allStatistic = await _fileReader.ReadFiles(files);
            return GetUsers(allStatistic);
        }*/

        private static IList<Currency> GetCurrency(IDictionary<int, IList<Currency>> allStatistic)
        {
            var users = new List<Currency>();

            var usersData = new List<Currency>();

 /*           foreach (var day in allStatistic)
            {
                foreach (Currency data in day.Value)
                {
                    var existedUsers = users.Where(u => u.UserName == data.User).ToList();
                    User user = null;
                    if (existedUsers.Count == 0)
                    {
                        user = new User
                        {
                            UserName = data.User,
                            UserData = new Dictionary<int, Currency>()
                        };
                        user.UserData.Add(day.Key, data);
                        users.Add(user);
                    }
                    else
                    {
                        user = existedUsers[0];
                        user.UserData.Add(day.Key, data);
                    }
                }
            }*/

            //GetTheAverageMaximumAndMinimumResults(users);

            return users;
        }

/*        private static void GetTheAverageMaximumAndMinimumResults(List<Currency> users)
        {
            foreach (var user in users)
            {
                var steps = user.UserData.Select(u => u.Value.Steps);
                user.AverageStepsNumber = (int)steps.Average();
                user.TheBestResult = steps.Max();
                user.TheWorstResult = steps.Min();
            }
        }*/
    }
}
