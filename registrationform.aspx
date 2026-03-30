<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrationform.aspx.cs" Inherits="Electronicsmgtsystem_071.registrationform" %>

<html>
<head runat="server">
    <title>Electronic Device Management</title>

    
</head>
<body>
    <div style="width:100%;padding:10px;background-color:#f5f5f5;">
        Welcome,
        <asp:Label ID="lblTopEmail" runat="server" Font-Bold="true"></asp:Label>
        </div>

<form id="form1" runat="server">

<asp:HiddenField ID="d_Id" runat="server" />

<center>
<table class="formTable" border="1">

    <tr>
        <th colspan="2">Electronic Device Management</th>
    </tr>

    <tr>
        <td class="label">Type</td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server"
                CssClass="textbox"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rbt_type" runat="server" ControlToValidate="ddlType" ErrorMessage="Select the type " ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td class="label">Brand</td>
        <td>
            <asp:DropDownList ID="ddlBrand" runat="server"
                CssClass="textbox">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rbt_brand" runat="server" ControlToValidate="ddlBrand" ErrorMessage="Select the brand" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td class="label">Model</td>
        <td>
            <asp:TextBox ID="txtModel" runat="server"
                CssClass="textbox" />
            <asp:RequiredFieldValidator ID="rf_model" runat="server" ControlToValidate="txtModel" ErrorMessage="Write the model" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td class="label">Description</td>
        <td>
            <asp:TextBox ID="txtDescription" runat="server"
                CssClass="textbox"
                TextMode="MultiLine"
                Rows="3" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription" ErrorMessage="Must write description" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td class="label">Price</td>
        <td>
            <asp:TextBox ID="txtPrice" runat="server"
                CssClass="textbox" />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price must be greter than 0" ForeColor="Red" Operator="GreaterThan" Type="Double" ValueToCompare="0"></asp:CompareValidator>
        </td>
    </tr>

    <tr>
        <td class="label">Quantity</td>
        <td>
            <asp:TextBox ID="txtQuantity" runat="server"
                CssClass="textbox" />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity must be greter than 0" ForeColor="Red" Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
        </td>
    </tr>

    <tr>
        <td class="label">Color</td>
        <td>
            <asp:RadioButton ID="rbt_blue" runat="server" GroupName="color" Text="Blue" />
            <asp:RadioButton ID="rbt_black" runat="server" GroupName="color" Text="Black" />
            <asp:RadioButton ID="rbt_gold" runat="server" GroupName="color" Text="Gold" />
            <asp:RadioButton ID="rbt_rosegold" runat="server" GroupName="color" Text="Rose Gold" />

        </td>
    </tr>

    <tr>
        <td class="label">Accessories</td>
        <td>
            <asp:CheckBox ID="chk_charger" runat="server" Text="Charger" />
             <asp:CheckBox ID="chk_headphone" runat="server" Text="Headphones" />
             <asp:CheckBox ID="chk_touchpen" runat="server" Text="Touch Pen" />
             <asp:CheckBox ID="chk_wirelessmouse" runat="server" Text="Wireless Mouse" />
        </td>
    </tr>
    <tr>
        <td>Upload Image :</td>
        <td>

            <asp:FileUpload ID="fu_image" runat="server" />
           
            </td>
    </tr>

    <tr>
        <td colspan="2" class="center">
            <asp:Button ID="btnSubmit" runat="server"
                Text="Save"
                CssClass="btn"
                OnClick="btnSubmit_Click" />

            &nbsp;&nbsp;

            <asp:Button ID="btnReset" runat="server"
                Text="Reset"
                CssClass="btn"
                OnClick="btnReset_Click" />
            <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" Text="Update" />
        </td>
    </tr>

</table>
</center>
<br /><br />


<center>
    <b>Search by Brand :</b>
    <asp:DropDownList ID="ddlSearchBrand" runat="server"
        AutoPostBack="true"
        OnSelectedIndexChanged="ddlSearchBrand_SelectedIndexChanged">
    </asp:DropDownList>
</center>

<br />


<center>
<asp:GridView ID="gv_display" runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="d_id"
    AutoGenerateSelectButton="True"
    AutoGenerateDeleteButton="True"
    OnSelectedIndexChanged="gv_display_SelectedIndexChanged"
    OnRowDeleting="gv_display_RowDeleting"
    BorderWidth="1">

    <Columns>
        <asp:BoundField DataField="d_id" HeaderText="ID" />
        <asp:BoundField DataField="BrandName" HeaderText="Brand" />
        <asp:BoundField DataField="model" HeaderText="Model" />
        <asp:BoundField DataField="description" HeaderText="Description" />
        <asp:BoundField DataField="price" HeaderText="Price" />
        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
        <asp:BoundField DataField="color" HeaderText="Color" />
        <asp:BoundField DataField="accessories" HeaderText="Accessories" />
        <asp:ImageField DataImageUrlField="image_path"
            HeaderText="Image"
            ControlStyle-Width="80"
            ControlStyle-Height="80"/>
    </Columns>

</asp:GridView>
</center>

<br /><br />

</form>
</body>
</html>