﻿namespace BLL.Services.Abstract
{
    public interface IAppliedCouponService
    {
		(bool success, string message) ApplyCoupon(long userId, long productId, long couponId);
		List<AppliedCoupon> GetAppliedCouponsByUser(long userId);
		List<AppliedCoupon> GetAppliedCouponsByProduct(long productId);
		(bool success, string message) RemoveAppliedCoupon(long userId, long productId, long couponId);
	}
}
