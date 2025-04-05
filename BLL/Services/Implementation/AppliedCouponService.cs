
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Services.Implementation
{
	public class AppliedCouponService : IAppliedCouponService
	{
		private readonly IAppliedCouponRepo _appliedCouponRepo;
		private readonly IMapper _mapper;

		public AppliedCouponService(IAppliedCouponRepo appliedCouponRepo, IMapper mapper)
		{
			_appliedCouponRepo = appliedCouponRepo;
			_mapper = mapper;
		}
		public (bool success, string message) ApplyCoupon(string userId, long productId, long couponId)
		{
			if (_appliedCouponRepo.Exists(userId ,productId ,couponId))
			{
				return (false, "Coupon already applied to this product.");
			}

			var coupon = _appliedCouponRepo.Get(c=>c.CouponId == couponId);
			if (coupon == null || coupon.Coupon.ExpiredAt < DateTime.Now)
			{
				return (false, "Invalid or expired coupon.");
			}

			var appliedCoupon = new AppliedCoupon(userId, productId, couponId);
			_appliedCouponRepo.Create(appliedCoupon);
			return (true, "Coupon applied successfully.");
		}

		public List<AppliedCoupon> GetAppliedCouponsByProduct(long productId)
		{
			return _appliedCouponRepo.GetByProduct(productId);
		}

		public List<AppliedCoupon> GetAppliedCouponsByUser(string userId)
		{
			return _appliedCouponRepo.GetByUser(userId);
		}

		public (bool success, string message) RemoveAppliedCoupon(string userId, long productId, long couponId)
		{
			try
			{
				if (!_appliedCouponRepo.Exists(userId, productId, couponId))
					return (false, "Applied Coupon not found!");

				_appliedCouponRepo.RemoveAppliedCoupon(userId, productId, couponId);
				return (true, "Applied Coupon removed successfully.");
			}
			catch (Exception ex)
			{
				return (false, $"An error occurred: {ex.Message}");
			}
		}
	}
}
