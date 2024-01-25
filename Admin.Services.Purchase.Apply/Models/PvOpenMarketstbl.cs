using System;
using System.Collections.Generic;

namespace Admin.Services.Purchase.Apply.Models;

public partial class PvOpenMarketstbl
{
    public int Id { get; set; }

    public string? PurchaseAmount { get; set; }

    public string? TokenAmount { get; set; }

    public string? WithholdAmount { get; set; }

    public string? SellerContactNumber { get; set; }

    public string? SellerEmailAddress { get; set; }

    public string? VehicleNumber { get; set; }

    public string? PaymentProof { get; set; }

    public string? SellerAdhaar { get; set; }

    public string? SellerPan { get; set; }

    public string? PictureOfOriginalRc { get; set; }

    public string? OdometerPicture { get; set; }

    public string? VehiclePictureFromFront { get; set; }

    public string? VehiclePictureFromBack { get; set; }

    public int UserInfoId { get; set; }
}
