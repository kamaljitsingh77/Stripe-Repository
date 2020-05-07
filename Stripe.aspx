<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stripe.aspx.cs" Inherits="StripePaymentSample.Stripe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnPayment" runat="server" Text="Pay with stripe" OnClick="btnPayment_Click"  /> 
            <br />
            <asp:Label ID="paymentmessage" runat="server" ></asp:Label>
        </div>
    </form>
</body>
</html>
