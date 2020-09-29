using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedFixator.Models
{

    public interface IFixationRepository
    {
        void Create(Fixation fixation);
        List<Fixation> GetFixations();
        List<Fixation> GetFixationsByDateAndSpeed(DateTime DateTime, double Speed);
        List<Fixation> GetMaxAndMinSpeedbyDate(DateTime Datetime);
    }

    public class FixationRepository: IFixationRepository
    {
        string connectionString = null;
        public FixationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Fixation fixation)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Fixations (DateTime, VechicleNumber,Speed) VALUES(@DateTime, @VechicleNumber,@Speed)";
                db.Execute(sqlQuery, fixation);
            }
        }

        public List<Fixation> GetFixationsByDateAndSpeed(DateTime DateTime, double Speed)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "select * from Fixations where Speed > @Speed and cast(DateTime as DATE) = cast(@Datetime as Date)";
                return db.Query<Fixation>(sqlQuery, new { DateTime = DateTime, Speed = Speed }).ToList();
            }
        }

        public List<Fixation> GetFixations()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Fixation>("SELECT * FROM Fixations").ToList();
            }
        }

        public List<Fixation> GetMaxAndMinSpeedbyDate(DateTime Datetime)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from Fixations WHERE speed = (SELECT max(Speed) as maxSpeed FROM Fixations where cast(DateTime as DATE) = cast(@DateTime as Date)) or Speed = (select min(Speed) as minSpeed FROM Fixations where cast(DateTime as DATE) = cast(@Datetime as Date))";
                return db.Query<Fixation>(sqlQuery, new { Datetime }).ToList();
            }
        }
    }
}
