﻿using System;
using System.Web.Mvc;
using Framework.Interfaces.Business;

namespace Framework.MVC.Controllers
{
	public abstract class PersistanceController<TEntity, TBusiness> : Controller
		where TEntity : class, new()
		where TBusiness : IBusiness<TEntity>, new()
	{
		protected Lazy<TBusiness> Business = new Lazy<TBusiness>();

		public virtual ActionResult Index()
		{
			return View(this.Business.Value.All());
		}

		public virtual ViewResult Details(int id)
		{
			TEntity entity = this.Business.Value.Get(id);
			DetailsGetPrerender(entity);

			return View(entity);
		}

		public virtual ActionResult Create()
		{
			TEntity entity = null;
			CreateGetPrerender(entity);

			if (entity != null)
				return View(entity);

			return View();
		}

		[HttpPost]
		public virtual ActionResult Create(TEntity entity)
		{
			if (ModelState.IsValid)
			{
				try
				{
					CreatePost(entity);
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}

			CreateGetPrerender();

			return View(entity);
		}

		public virtual ActionResult Edit(int id)
		{
			TEntity entity = this.Business.Value.Get(id);
			EditGetPrerender(entity);

			return View(entity);
		}

		[HttpPost]
		public virtual ActionResult Edit(TEntity entity)
		{
			if (ModelState.IsValid)
			{
				try
				{
					EditPost(entity);
					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}

			EditGetPrerender(entity);

			return View(entity);
		}

		public virtual ActionResult Delete(int id)
		{
			TEntity entity = this.Business.Value.Get(id);
			DeleteGetPrerender(entity);

			return View(entity);
		}

		[HttpPost, ActionName("Delete")]
		public virtual ActionResult DeleteConfirmed(int id)
		{
			TEntity entity = new TEntity();

			try
			{
				entity = this.Business.Value.Get(id);
				DeletePost(entity);

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			DeleteGetPrerender(entity);

			return View("Delete", entity);
		}

		public virtual void CreatePost(TEntity entity)
		{
			this.Business.Value.Create(entity);
		}

		public virtual void EditPost(TEntity entity)
		{
			this.Business.Value.Update(entity);
		}

		public virtual void DeletePost(TEntity entity)
		{
			this.Business.Value.Delete(entity);
		}

		public virtual void CreateGetPrerender(TEntity entity = null)
		{
		}

		public virtual void EditGetPrerender(TEntity entity)
		{
		}

		public virtual void DeleteGetPrerender(TEntity entity)
		{
		}

		public virtual void DetailsGetPrerender(TEntity entity)
		{
		}

		public virtual void CopyGetPrerender(TEntity entity)
		{
		}

		public virtual ActionResult Copy(int id)
		{
			TEntity entity = this.Business.Value.Get(id);
			CopyGetPrerender(entity);
			CreateGetPrerender(entity);

			return View("Create", entity);
		}
	}
}