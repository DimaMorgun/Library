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
            _unitOfWork = new UnitOfWork();
        }

        public void Create(MagazineViewModel magazine)
        {
            Magazine magazineModel = Mapper.Map<MagazineViewModel, Magazine>(magazine);

            _unitOfWork.Magazines.Create(magazineModel);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Magazines.Delete(id);
            _unitOfWork.Save();
        }

        public MagazineViewModel GetByid(int id)
        {
            Magazine magazineModel = _unitOfWork.Magazines.GetByid(id);

            MagazineViewModel magazineViewModel = Mapper.Map<Magazine, MagazineViewModel>(magazineModel);

            return magazineViewModel;
        }

        public List<MagazineViewModel> GetList()
        {
            List<Magazine> allMagazinesModel = _unitOfWork.Magazines.GetList();

            List<MagazineViewModel> allMagazinesViewModel = Mapper.Map<List<Magazine>, List<MagazineViewModel>>(allMagazinesModel);

            return allMagazinesViewModel;
        }

        public void Update(MagazineViewModel magazine)
        {
            Magazine magazineModel = _unitOfWork.Magazines.GetByid(magazine.MagazineId);
            magazineModel.Name = magazine.Name;
            magazineModel.Number = magazine.Number;
            magazineModel.YearOfPublishing = magazine.YearOfPublishing;

            _unitOfWork.Magazines.Update(magazineModel);
            _unitOfWork.Save();
        }

        public void SaveToJSON(int id)
        {
            Magazine magazineModel = _unitOfWork.Magazines.GetByid(id);

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
