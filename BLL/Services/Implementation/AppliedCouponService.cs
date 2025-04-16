
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

        public async Task<(bool success, string message)> ApplyCouponAsync(string userId, long productId, long couponId)
        {
            try
            {
                // check if the coupon exists
                var coupon = await couponRepo.GetAsync(c => c.Id == couponId);
                if (coupon == null)
                {
                    return (false, "Coupon not found.");
                }
                // check if the coupon has expired
                if (coupon.ExpiredAt.HasValue && coupon.ExpiredAt.Value < DateTime.Now)
                {
                    return (false, "Coupon has expired.");
                }
                // check if the coupon is assigned to the product
                var appliedCoupon = await appliedCouponRepo.GetAsync(ac => ac.ProductId == productId && ac.CouponId == couponId);
                if (appliedCoupon == null)
                {
                    return (false, "This coupon is not valid for the selected product.");
                }
                // check if the user has already used this coupon for the product
                var usedCoupon = await appliedCouponRepo.GetAsync(uc => uc.UserId == userId && uc.CouponId == couponId && uc.ProductId == productId);
                if (usedCoupon != null)
                {
                    return (false, "You have already used this coupon for this product.");
                }

                var newAppliedCoupon = new AppliedCoupon(userId, productId, couponId);
                var result = await appliedCouponRepo.CreateAsync(newAppliedCoupon);
                if (result.Item1)
                    return (true, "Coupon applied successfully.");
                return (false, result.Item2);
            } catch(Exception ex)
            {
                return (false, ex.Message);
            }
            
        }

        public async Task<(bool success, string message)> AssignCouponToProductAsync(string userId, long productId, long couponId)
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

        public async Task<(bool success, string message)> CreateAppliedCouponAsync(AppliedCoupon appliedCoupon)
        {
            try
            {
                var result = await appliedCouponRepo.CreateAsync(appliedCoupon);
                if (!result.Item1)
                    return (false, result.Item2);
                return (true, "Cteated Successfully");
            } catch(Exception ex)
            {
                return (false,ex.Message);
            }
        }
        public async Task<List<AppliedCoupon>> GetAllAppliedCouponsAsync(Expression<Func<AppliedCoupon, bool>>? filte = null, params Expression<Func<AppliedCoupon, object>>[] includeProperties)
        {
            return await appliedCouponRepo.GetAllAsync(filte, includeProperties);

        }
        public async Task<AppliedCoupon> GetAppliedCouponAsync(Expression<Func<AppliedCoupon, bool>>? filte = null)
        {
            return await appliedCouponRepo.GetAsync(filte);
        }

        public async Task<(bool success, string message)> RemoveAppliedCouponAsync(string userId, long productId, long couponId)
        {
            try
            {
                var coupon = await appliedCouponRepo.GetAsync(ac => ac.UserId == userId && ac.ProductId == productId && ac.CouponId == couponId);
                if (coupon == null)
                    return (false, "Applied Coupon not found!");

                var result = await appliedCouponRepo.RemoveAppliedCouponAsync(userId, productId, couponId);
                if(result)
                    return (true, "Applied Coupon removed successfully.");
                return (false, "SomeThing Went Wrong While Removing From LocalDB");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }
    }
}
