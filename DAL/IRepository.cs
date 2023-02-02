using DAL.Model;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public interface IRepository
    {
        Apartment GetApartment(int id);
        IList<Apartment> GetApartments(int? statusId, int? cityId, int? order);
        int CreateApartment(Apartment apartment);
        void UpdateApartment(Apartment apartment);
        void DeleteApartment(int id);
        IList<Tag> GetApartmantTags(int apartmantID);
        IList<ApartmentPicture> GetApartmentPictures(int apartmantId);
        IList<City> GetCities();
        IList<Tag> GetTags();
        void CreateTag(Tag tag);
        void DeleteTag(int idtag);
        IList<ApartmentOwner> GetApartmentOwners();
        int CreateApartmentPicture(ApartmentPicture ac);
        int CreateUser(User user);
        string GetUserRole (string userName, string passwordHash);
        List<User> GetAllUsers();
        User GetUser(string userName, string password);
        User GetUserByID(int id);
        void CreateApartmentReservation(ApartmentReservation ar);
        ApartmentReservation GetApartmentReservation(int iDApartment);
        void DeleteApartmentReservation(int iDApartment);
        int CreateReview(ApartmentReview apartmentReview);
        List<Pictures> GetPictures(int apartmentId);
    }
}