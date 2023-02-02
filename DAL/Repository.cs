using DAL.Model;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Linq;
using System.Security;
using System.Data.Common;
using static DAL.Model.Apartment;
using DAL.ViewModel;
using System.Collections;
using DAL.Utils;
using Microsoft.ApplicationBlocks.Data;

namespace DAL
{

    internal class Repository : IRepository
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public int CreateApartment(Apartment apartment)
        {
            DataTable PicList = GetPicList(apartment);
            DataTable KeyList = GetKeyList(apartment);
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.CreateApartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier).Value = apartment.Guid;
                    command.Parameters.Add("@ownerId", SqlDbType.Int).Value = apartment.OwnerId.Id;
                    command.Parameters.Add("@typeId", SqlDbType.Int).Value = apartment.TypeId;
                    command.Parameters.Add("@statusId", SqlDbType.Int).Value = apartment.StatusId;
                    command.Parameters.Add("@cityId", SqlDbType.Int).Value = apartment.CityId.Id;
                    command.Parameters.Add("@address", SqlDbType.NVarChar, 250).Value = apartment.Address;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 250).Value = apartment.Name;
                    command.Parameters.Add("@price", SqlDbType.Money).Value = apartment.Price;
                    command.Parameters.Add("@maxAdults", SqlDbType.Int).Value = apartment.MaxAdults;
                    command.Parameters.Add("@maxChildren", SqlDbType.Int).Value = apartment.MaxChildren;
                    command.Parameters.Add("@totalRooms", SqlDbType.Int).Value = apartment.TotalRooms;
                    command.Parameters.Add("@beachDistance", SqlDbType.Int).Value = apartment.BeachDistance;

                    command.Parameters.Add("@tags", SqlDbType.Structured).Value = KeyList;
                    command.Parameters.Add("@pictures", SqlDbType.Structured).Value = PicList;


                    command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    return (int)command.Parameters["@id"].Value;
                }
            }
        }

        private DataTable GetPicList(Apartment apartment)
        {

            DataTable PictureList = new DataTable();
            PictureList.Columns.Add("Id", typeof(int));
            PictureList.Columns.Add("Path", typeof(string));
            PictureList.Columns.Add("Name", typeof(string));
            PictureList.Columns.Add("IsRepresentative", typeof(bool));
            PictureList.Columns.Add("DoDelete", typeof(bool));
            
            foreach (var item in apartment.Pictures ?? new List<Pictures>())
            {
                DataRow dr = PictureList.NewRow();

                dr["Path"] = item.Path;
                dr["Name"] = item.Name;
                dr["IsRepresentative"] = item.IsRepresentative;
                dr["DoDelete"] = item.DoDelete;



                PictureList.Rows.Add(dr);
            }

            return PictureList;
        }

        private DataTable GetKeyList(Apartment apartment)
        {
            DataTable KeyList = new DataTable();
            KeyList.Columns.Add("Key", typeof(int));
            if (apartment.Tag != null)
            {
                foreach (var item in apartment.Tag)
                {
                    DataRow dr = KeyList.NewRow();
                    dr["Key"] = item.Id;
                    KeyList.Rows.Add(dr);
                }
            }
            else
            {
                KeyList = null;
            }
            return KeyList;
        }

        public int CreateApartmentPicture(ApartmentPicture ac)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("dbo.CreatePicture", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier).Value = ac.Guid;
                    command.Parameters.Add("@createdat", SqlDbType.DateTime).Value = DateTime.Now;
                    command.Parameters.Add("@aptid", SqlDbType.Int).Value = ac.ApartmentId.IDApartment;
                    command.Parameters.Add("@path", SqlDbType.NVarChar, 200).Value = ac.Path;
                    command.Parameters.Add("@base64", SqlDbType.NVarChar, 200).Value = ac.Base64Content;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, -1).Value = ac.Name;
                    command.Parameters.Add("@isrep", SqlDbType.Bit).Value = ac.IsRepresentative;
                    command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@id"].Value;
                }
            }
        }

        public void CreateApartmentReservation(ApartmentReservation ar)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("MakeApartentReservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier).Value = ar.Guid;
                    command.Parameters.Add("@createdat", SqlDbType.DateTime).Value = ar.CreatedAt;
                    command.Parameters.Add("@aprtmentid", SqlDbType.Int).Value = ar.ApartmentId.IDApartment;
                    command.Parameters.Add("@details", SqlDbType.NVarChar, 299).Value = ar.Details;
                    command.Parameters.Add("@userid", SqlDbType.Int).Value = ar.UserId.Id;
                    command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                }
            }
        }
        public void CreateTag(Tag tag)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("CreateTag", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier).Value = tag.Guid;
                    command.Parameters.Add("@createdat", SqlDbType.DateTime).Value = tag.CreatedAt;
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = tag.Name;
                    command.Parameters.Add("@nameeng", SqlDbType.NVarChar).Value = tag.NameEng;
                    command.ExecuteNonQuery();


                }
            }
        }

        public int CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CreateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier).Value = user.Guid;
                    command.Parameters.Add("@createdat", SqlDbType.DateTime).Value = user.CreatedAt;
                    command.Parameters.Add("@deletedat", SqlDbType.DateTime).Value = DBNull.Value;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 256).Value = user.Email;
                    command.Parameters.Add("@emailconfirmed", SqlDbType.Bit).Value = user.EmailConfirmed;
                    command.Parameters.Add("@passwordhash", SqlDbType.NVarChar, -1).Value = user.PasswordHash;
                    command.Parameters.Add("@securitystam", SqlDbType.NVarChar, -1).Value = DBNull.Value;
                    command.Parameters.Add("@phonenumber", SqlDbType.NVarChar, -1).Value = user.PhoneNumber;
                    command.Parameters.Add("@phonenumberconfirmed", SqlDbType.Bit).Value = user.PhoneNumberConfirmed;
                    command.Parameters.Add("@lockoutenddateutc", SqlDbType.DateTime).Value = DBNull.Value;
                    command.Parameters.Add("@lockoutenabled", SqlDbType.Bit).Value = user.LockoutEnabled;
                    command.Parameters.Add("@accessfailedcount", SqlDbType.Int).Value = user.AccessFailedCount;
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 256).Value = user.UserName;
                    command.Parameters.Add("@address", SqlDbType.NVarChar, 256).Value = user.Address;


                    command.ExecuteNonQuery();

                    return (int)command.Parameters["@id"].Value;
                }
            }
        }

        public void DeleteApartment(int id)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.DeleteApartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteApartmentReservation(int iDApartment)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.DeleteApartmentReservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id", SqlDbType.Int).Value = iDApartment;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTag(int idtag)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.DeleteTag", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id", SqlDbType.Int).Value = idtag;

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Guid = reader.GetGuid(reader.GetOrdinal("Guid")),

                                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),

                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    EmailConfirmed = reader.GetBoolean(reader.GetOrdinal("EmailConfirmed")),

                                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                    PhoneNumberConfirmed = reader.GetBoolean(reader.GetOrdinal("PhoneNumberConfirmed")),

                                    LockoutEnabled = reader.GetBoolean(reader.GetOrdinal("LockoutEnabled")),
                                    AccessFailedCount = reader.GetInt32(reader.GetOrdinal("AccessFailedCount")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    Address = reader.GetString(reader.GetOrdinal("Address"))
                                };
                                users.Add(user);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                        return users;
                    }
                }
            }
        }



        public IList<Tag> GetApartmantTags(int apartmantID)
        {
            List<Tag> tags = new List<Tag>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetApartmentTags", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@apartmentId", apartmantID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tags.Add(new Tag
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });

                        }
                    }
                }
            }

            return tags;
        }


        public Apartment GetApartment(int id)
        {
            Apartment apartment = null;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetApartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ownerID = reader.GetInt32(4);
                            string ownerName = reader.GetString(5);
                            int statusId = reader.GetInt32(7);
                            string statusName = reader.GetString(8);
                            int cityId = reader.GetInt32(9);
                            string cityName = reader.GetString(10);
                            apartment = new Apartment
                            {
                                IDApartment = reader.GetInt32(0),
                                Guid = reader.GetGuid(1),
                                CreatedAt = reader.GetDateTime(2),
                                DeletedAt = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                                TypeId = reader.GetInt32(6),
                                OwnerId = GetApartmentOwners().ToList().Find(o => o.Id == ownerID),
                                StatusId = (Apartment.Status)Enum.Parse(typeof(Status), statusName),
                                CityId = GetCities().ToList().Find(c => c.Id == cityId),
                                Address = reader.GetString(11),
                                Name = reader.GetString(12),
                                NameEng = reader.IsDBNull(13) ? null : reader.GetString(13),
                                Price = reader.GetDecimal(14),
                                MaxAdults = reader.GetInt32(15),
                                MaxChildren = reader.GetInt32(16),
                                TotalRooms = reader.GetInt32(17),
                                BeachDistance = reader.GetInt32(18)
                            };


                        }
                    }
                }
            }
            return apartment;
        }



        public IList<ApartmentOwner> GetApartmentOwners()
        {
            List<ApartmentOwner> apartmentOwners = new List<ApartmentOwner>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetApartmentOwners", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            apartmentOwners.Add(new ApartmentOwner
                            {
                                Id = reader.GetInt32(0),
                                Guid = reader.GetGuid(1),
                                Name = reader.GetString(2),
                            });

                        }
                    }
                }
            }

            return apartmentOwners;
        }



        public IList<ApartmentPicture> GetApartmentPictures(int apartmantID)
        {
            List<ApartmentPicture> pictures = new List<ApartmentPicture>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetApartmentPictures", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@apartmentId", apartmantID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pictures.Add(new ApartmentPicture
                            {
                                Id = reader.GetInt32(0),
                                Guid = reader.GetGuid(1),
                                Name = reader.GetString(2),
                                Path = reader.GetString(3),
                                IsRepresentative = reader.GetBoolean(4)
                            });
                        }
                    }
                }
                return pictures;
            }
        }

        public ApartmentReservation GetApartmentReservation(int iDApartment)
        {
            ApartmentReservation ar = new ApartmentReservation();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetApartmentReservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", iDApartment);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            ar.Guid = reader.GetGuid(1);
                            ar.CreatedAt = reader.GetDateTime(2);

                            ar.ApartmentId = GetApartment(reader.GetInt32(3));
                            ar.Details = reader.GetString(4);

                            ar.UserId = GetAllUsers().Find(u => u.Id == reader.GetInt32(5));
                            ar.UserName = reader.GetString(6);
                            ar.UserEmail = reader.GetString(7);
                            ar.UserPhone = reader.GetString(8);
                            ar.UserAddress = reader.GetString(9);

                        }
                    }
                }
            }
            return ar;
        }

        public IList<Apartment> GetApartments(int? statusId, int? cityId, int? order)
        {
            IList<Apartment> apartments = new List<Apartment>();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetApartments", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@statusId", statusId);
                    command.Parameters.AddWithValue("@cityId", cityId);
                    command.Parameters.AddWithValue("@order", order);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ownerID = reader.GetInt32(4);
                            string ownerName = reader.GetString(5);
                            int statussId = reader.GetInt32(7);
                            string statusName = reader.GetString(8);
                            int cityyId = reader.GetInt32(9);
                            string cityName = reader.GetString(10);

                            apartments.Add(new Apartment
                            {
                                IDApartment = reader.GetInt32(0),
                                Guid = reader.GetGuid(1),
                                CreatedAt = reader.GetDateTime(2),
                                DeletedAt = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                                TypeId = reader.GetInt32(6),
                                OwnerId = GetApartmentOwners().ToList().Find(o => o.Id == ownerID),
                                StatusId = (Apartment.Status)Enum.Parse(typeof(Apartment.Status), statusName),
                                CityId = GetCities().ToList().Find(c => c.Id == cityyId),
                                Address = reader.GetString(11),
                                Name = reader.GetString(12),
                                NameEng = reader.IsDBNull(13) ? null : reader.GetString(13),
                                Price = reader.GetDecimal(14),
                                MaxAdults = reader.GetInt32(15),
                                MaxChildren = reader.GetInt32(16),
                                TotalRooms = reader.GetInt32(17),
                                BeachDistance = reader.GetInt32(18)

                            });
                        }
                    }

                }
            }
            return apartments;

        }

        public IList<City> GetCities()
        {
            List<City> cities = new List<City>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetCities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                Id = reader.GetInt32(0),
                                Guid = reader.GetGuid(1),
                                Name = reader.GetString(2)

                            });

                        }
                    }
                }
            }

            return cities;
        }


        public IList<Tag> GetTags()
        {
            List<Tag> tags = new List<Tag>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetTags", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tags.Add(new Tag
                            {
                                Id = reader.GetInt32(0),
                                Guid = reader.GetGuid(1),
                                Name = reader.GetString(2)

                            });
                        }
                    }
                }
            }
            return tags;
        }

        public User GetUser(string userName, string password)
        {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetOneUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 300).Value = userName;
                    command.Parameters.Add("@passwordhash", SqlDbType.NVarChar, 200).Value = password;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        try
                        {

                            while (reader.Read())
                            {
                                user.Id = reader.GetInt32(0);
                                user.Guid = reader.GetGuid(reader.GetOrdinal("Guid"));
                                user.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));

                                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                                user.EmailConfirmed = reader.GetBoolean(reader.GetOrdinal("EmailConfirmed"));
                                user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                                user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                                user.PhoneNumberConfirmed = reader.GetBoolean(reader.GetOrdinal("PhoneNumberConfirmed"));
                                user.LockoutEnabled = reader.GetBoolean(reader.GetOrdinal("LockoutEnabled"));
                                user.AccessFailedCount = reader.GetInt32(reader.GetOrdinal("AccessFailedCount"));
                                user.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                                user.Address = reader.GetString(reader.GetOrdinal("Address"));
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        return user;

                    }
                }
            }
        }
        public User GetUserByID(int id)
        {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int, 300).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        try
                        {

                            while (reader.Read())
                            {
                                user.Id = reader.GetInt32(0);
                                user.Guid = reader.GetGuid(reader.GetOrdinal("Guid"));
                                user.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                                user.EmailConfirmed = reader.GetBoolean(reader.GetOrdinal("EmailConfirmed"));
                                user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                                user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                                user.PhoneNumberConfirmed = reader.GetBoolean(reader.GetOrdinal("PhoneNumberConfirmed"));
                                user.LockoutEnabled = reader.GetBoolean(reader.GetOrdinal("LockoutEnabled"));
                                user.AccessFailedCount = reader.GetInt32(reader.GetOrdinal("AccessFailedCount"));
                                user.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                                user.Address = reader.GetString(reader.GetOrdinal("Address"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        return user;

                    }
                }
            }
        }
        public string GetUserRole(string userName, string passwordHash)
        {
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();

                using (var command = new SqlCommand("SelectUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 256).Value = userName;
                    command.Parameters.Add("@passwordhash", SqlDbType.NVarChar, -1).Value = passwordHash;
                    command.Parameters.Add("@userrole", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    return (string)command.Parameters["@userrole"].Value;
                }
            }
        }


        public void UpdateApartment(Apartment apartment)
        {
            DataTable PictureList = GetPicList(apartment);
            DataTable KeyList = GetKeyList(apartment);
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.UpdateApartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.Add("@id", SqlDbType.Int).Value = apartment.IDApartment;
                    command.Parameters.Add("@guid", SqlDbType.UniqueIdentifier).Value = apartment.Guid;
                    command.Parameters.Add("@ownerId", SqlDbType.Int).Value = apartment.OwnerId.Id;
                    command.Parameters.Add("@typeId", SqlDbType.Int).Value = apartment.TypeId;
                    command.Parameters.Add("@statusId", SqlDbType.Int).Value = apartment.StatusId;
                    command.Parameters.Add("@cityId", SqlDbType.Int).Value = apartment.CityId.Id;
                    command.Parameters.Add("@address", SqlDbType.NVarChar, 250).Value = apartment.Address;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 250).Value = apartment.Name;
                    command.Parameters.Add("@price", SqlDbType.Money).Value = apartment.Price;
                    command.Parameters.Add("@maxAdults", SqlDbType.Int).Value = apartment.MaxAdults;
                    command.Parameters.Add("@maxChildren", SqlDbType.Int).Value = apartment.MaxChildren;
                    command.Parameters.Add("@totalRooms", SqlDbType.Int).Value = apartment.TotalRooms;
                    command.Parameters.Add("@beachDistance", SqlDbType.Int).Value = apartment.BeachDistance;


                    command.Parameters.Add("@tags", SqlDbType.Structured).Value = KeyList;
                    command.Parameters.Add("@pictures", SqlDbType.Structured).Value = PictureList;


                    command.ExecuteNonQuery();
                }
            }
        }

        public int CreateReview(ApartmentReview ar)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CreateReview", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@apartmentid", SqlDbType.Int).Value = ar.ApartmentId;
                    command.Parameters.Add("@userid", SqlDbType.Int).Value = ar.UserId;
                    command.Parameters.Add("@details", SqlDbType.NVarChar, 1000).Value = ar.Details;
                    command.Parameters.Add("@stars", SqlDbType.Int).Value = ar.Stars;

                    SqlParameter outputParameter = command.Parameters.Add("@id", SqlDbType.Int);
                    outputParameter.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();


                    return (int)outputParameter.Value;
                }
            }
        }
    
        public List<Pictures> GetPictures(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));

            var ds = SqlHelper.ExecuteDataset(
            cs,
            CommandType.StoredProcedure,
            "dbo.GetApartmentPictures",
            commandParameters.ToArray());
            var pics = new List<Pictures>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                pics.Add(new Pictures
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Path = row["Path"].ToString(),
                    Name = row["Name"].ToString(),
                    IsRepresentative = bool.Parse(row["IsRepresentative"].ToString())
                });
            }
            return pics;
        }

    }
}