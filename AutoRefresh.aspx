<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoRefresh.aspx.cs" Inherits="AutoRefresh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
        setTimeout("window.location.href='AutoRefresh.aspx';", 120000)
    </script>
</head>
<body>
</body>
</html>
