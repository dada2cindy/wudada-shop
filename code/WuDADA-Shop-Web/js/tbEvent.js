
        function doKeyDown(obj) {
            switch (event.keyCode) {
                case 13:
                case 37:  //左
                    //alert("37");
                    if (obj.nextid != "") document.all(obj.preid).select();
                    break;
                case 38:  //上
                    //alert("38");
                    if (obj.preid != "") document.all(obj.upid).select();
                    break;
                case 39:
                    //alert("39"); //右
                    if (obj.nextid != "") { document.all(obj.nextid).select(); }
                    break;
                case 40: //下
                    //alert("40");
                    if (obj.nextid != "") document.all(obj.downid).select();
                    break;
            }
        }

        function focuscnt(obj) {
            if (obj.value == 0)
                document.all(obj.id).value = "";
        }
        function blurcnt(obj) {
            if (obj.value == "")
                document.all(obj.id).value = "0";
        }