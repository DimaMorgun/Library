using Library.DataAccessLayer.Connection;
using Library.DataAccessLayer.UnitOfWork;
using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;

using AutoMapper;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Web;

namespace Library.BusinessLogicLayer.Services
{
    public class MagazineService
    {
        private UnitOfWork _unitOfWork;

        public MagazineService()
        {
            _unitOfWork = new UnitOfWork(CurrentConnection.ConnectionString);
        }

        public void Insert(MagazineViewModel magazine)
        {
            Magazine magazineModel = Mapper.Map<MagazineViewModel, Magazine>(magazine);

            _unitOfWork.Magazines.Insert(magazineModel);
        }

        public MagazineViewModel Get(int id)
        {
            Magazine magazineModel = _unitOfWork.Magazines.Get(id);

            MagazineViewModel magazineViewModel = Mapper.Map<Magazine, MagazineViewModel>(magazineModel);

            return magazineViewModel;
        }

        public List<MagazineViewModel> GetAll()
        {
            List<Magazine> allMagazinesModel = _unitOfWork.Magazines.GetAll();

            List<MagazineViewModel> allMagazinesViewModel = Mapper.Map<List<Magazine>, List<MagazineViewModel>>(allMagazinesModel);

            return allMagazinesViewModel;
        }

        public void Update(MagazineViewModel magazine)
        {
            Magazine magazineModel = _unitOfWork.Magazines.Get(magazine.MagazineId);
            magazineModel.Name = magazine.Name;
            magazineModel.Number = magazine.Number;
            magazineModel.YearOfPublishing = magazine.YearOfPublishing;

            _unitOfWork.Magazines.Update(magazineModel);
        }

        public void Delete(int id)
        {
            var magazineModel = _unitOfWork.Magazines.Get(id);
            _unitOfWork.Magazines.Delete(magazineModel);
        }

        public void SaveToJSON(int id)
        {
            Magazine magazineModel = _unitOfWork.Magazines.Get(id);

            string json = JsonConvert.SerializeObject(
                magazineModel,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var path = HttpContext.Current.Server.MapPath("~/App_Data/Magazine.json");

            System.IO.File.WriteAllText(path, json);
        }
    }
}
