using Library.DataAccessLayer.UnitOfWork;
using Library.EntityLayer.Models;
using Library.ViewModelLayer.ViewModels;

using AutoMapper;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Web;

namespace Library.BusinessLogicLayer.Services
{
    public class BrochureService
    {
        private UnitOfWork _unitOfWork;

        public BrochureService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Insert(BrochureViewModel brochure)
        {
            Brochure brochureModel = Mapper.Map<BrochureViewModel, Brochure>(brochure);

            _unitOfWork.Brochures.Insert(brochureModel);
        }

        public BrochureViewModel Get(int id)
        {
            Brochure brochureModel = _unitOfWork.Brochures.Get(id);

            BrochureViewModel brochureViewModel = Mapper.Map<Brochure, BrochureViewModel>(brochureModel);

            return brochureViewModel;
        }

        public List<BrochureViewModel> GetAll()
        {
            List<Brochure> allBrochuresModel = _unitOfWork.Brochures.GetAll();

            List<BrochureViewModel> allBrochuresViewModel = Mapper.Map<List<Brochure>, List<BrochureViewModel>>(allBrochuresModel);

            return allBrochuresViewModel;
        }

        public void Update(BrochureViewModel Brochure)
        {
            Brochure brochureModel = _unitOfWork.Brochures.Get(Brochure.BrochureId);
            brochureModel.Name = Brochure.Name;
            brochureModel.NumberOfPages = Brochure.NumberOfPages;
            brochureModel.TypeOfCover = Brochure.TypeOfCover;

            _unitOfWork.Brochures.Update(brochureModel);
        }

        public void Delete(int id)
        {
            var brochureModel = _unitOfWork.Brochures.Get(id);
            _unitOfWork.Brochures.Delete(brochureModel);
        }

        public void SaveToJSON(int id)
        {
            Brochure brochureModel = _unitOfWork.Brochures.Get(id);

            string json = JsonConvert.SerializeObject(
                brochureModel,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            var path = HttpContext.Current.Server.MapPath("~/App_Data/Brochure.json");

            System.IO.File.WriteAllText(path, json);
        }
    }
}
