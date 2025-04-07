
using Microsoft.EntityFrameworkCore;
using System;

namespace BLL.Services.Implementation
{
	public class AppliedCouponService : IAppliedCouponService
	{
        private readonly IProductRepo productRepo;
        private readonly ICouponRepo couponRepo;
        private readonly IAppliedCouponRepo appliedCouponRepo;

        public AppliedCouponService(IProductRepo productRepo, ICouponRepo couponRepo, IAppliedCouponRepo appliedCouponRepo)
		{
            this.productRepo = productRepo;
            this.couponRepo = couponRepo;
            this.appliedCouponRepo = appliedCouponRepo;
        }

        public Task<(bool success, string message)> ApplyCoupon(string userId, long productId, long couponId)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool success, string message)> AssignCouponToProduct(string userId, long productId, long couponId)
        {
            // get product that belonge to the Vendor
            var product = await productRepo.GetAsync(p => p.Id == productId && p.UserId == userId);
            if (product == null)
                return (false, "Product not found or does not belong to this vendor.");
            var coupon = await couponRepo.GetAsync(c => c.Id == couponId);
            if (coupon == null)
                return (false, "Coupon not found.");
            var existingAssignment = await appliedCouponRepo.GetAsync(pc => pc.ProductId == productId && pc.CouponId == couponId);
            if (existingAssignment != null)
                return (false, "Coupon is already assigned to this product.");
            var appliedCoupon = new AppliedCoupon(userId, productId, couponId);
            var result = await appliedCouponRepo.CreateAsync(appliedCoupon);

            if (result.Item1)
                return (true, "Coupon assigned to product successfully.");
            else
                return (false, result.Item2 ?? "Failed to assign coupon.");

        }

        public Task<List<AppliedCoupon>> GetAllAppliedCoupons(Expression<Func<AppliedCoupon, bool>>? filte = null, params Expression<Func<AppliedCoupon, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<AppliedCoupon> GetAppliedCoupon(Expression<Func<AppliedCoupon, bool>>? filte = null)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, string message)> RemoveAppliedCoupon(string userId, long productId, long couponId)
        {
            throw new NotImplementedException();
        }
    }
}
