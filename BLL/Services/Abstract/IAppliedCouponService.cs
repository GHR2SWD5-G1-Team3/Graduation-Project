using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IAppliedCouponService
    {
        bool ApplyCoupon(int cartId, string couponCode);
        void RemoveCoupon(int cartId);
        AppliedCoupon GetAppliedCoupon(int cartId);
    }
}
