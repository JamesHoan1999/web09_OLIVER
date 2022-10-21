
window.onload = function (){
    initEvents();
}

function initEvents(){
    //Hiển thị form thêm mới
    document.getElementById("btnOpenFormAddEmployee").addEventListener("click",btnOpenFormEmployeeDetailOnClick);
    //Ẩn form nhân viên
    document.getElementById("btnCloseFormEmployee").addEventListener("click",btnCloseFormEmployeeDetailOnClick);
    //Cất dữ liệu
    document.getElementById("btnSaveEmployee").addEventListener("click",btnSaveOnClick);


    //Xử lý SideBar thu gọn ,mở rộng
    document.getElementById("navbarIcon").addEventListener("click",handleSideBar);
    //
    document.getElementById("btnSaveEmployee").addEventListener("blur",tabindexHandle)


     


    //COMBOBOX
    //Open combobox phân trang
    document.getElementById("btnOpenComboboxPaging").addEventListener("click",btnOpenComboboxPagingOnClick);

    //Close combobox phân trang
    
    

    document.getElementsByClassName("data-item")[0].addEventListener("click",updateValueInputComboboxPaging);
    document.getElementsByClassName("data-item")[1].addEventListener("click",updateValueInputComboboxPaging);
    document.getElementsByClassName("data-item")[2].addEventListener("click",updateValueInputComboboxPaging);
    document.getElementsByClassName("data-item")[3].addEventListener("click",updateValueInputComboboxPaging);
    

    // document.getElementById("inputComboboxPaging").addEventListener("focusout",btnCloseComboboxPagingFocusOut);
 


}


/**
 * xử lý tabindex form nhân viên
 * Author:HoanOliver(21/10/2022)
 */

function tabindexHandle(){
    document.getElementById("txtEmployeeCode").focus();
}
/**
 * Thực hiện cất dữ liệu
 * Author : HoanOliver(19/10/2022)
 */

function btnSaveOnClick() {
    
    //Validate dữ liệu:
    var isValid =validateData();

    //Thu thập dữ liệu:
    var employee = {};
    var employeeCode = "";


    //Gọi api thực hiện cất dữ liệu:
    fetch("https://amis.manhnv.net/api/v1/Employees",
    {method:"POST",body: JSON.stringify(employee)})
        .then(res => res.json())
        .then(res => {
            console.log(res);

        })
        //Nếu xảy ra lỗi chạy vào catch
        .catch(res => {

        })
    //Kiểm tra kết quả trả về -> Đưa ra thông báo:
}


/**
 * Thực hiện validate dữ liệu
 * Author:HoanOliver(19/10/2022)
 */
function validateData(){
    //Các thông tin bắt buộc nhập
    var inputRequireds = getAllElementsWithAttribute("required");

   for (const input of inputRequireds) {
    var value =input.value;
    if(!value){
        //Add border màu đỏ
        input.classList.add('input--error');
   

    //Các thông tin đúng định dạng (email)

    //Ngày tháng

    }
    else {
        input.classList.remove('input--error');

    }

    
   }
   

   
}

/**
 * Ẩn form chi tiết nhân viên
 * Author:HoanOliver(19/10/2022)
 */
function btnCloseFormEmployeeDetailOnClick(){
    //Ẩn form chi tiết nhân viên
    document.getElementById("form-employeeDetail").style.display = "none";

    
}

/**
 * Hiển thị form chi tiết nhân viên
 * Author:HoanOliver(19/10/2022)
 */

 function btnOpenFormEmployeeDetailOnClick(){
    //Hiển thị form chi tiết nhân viên
    document.getElementById("form-employeeDetail").style.display = "flex";

    document.getElementById("txtEmployeeCode").classList.remove('input--error');
    document.getElementById("txtFullName").classList.remove('input--error');

    console.log('ád');
    //Focus vào mã nhân viên khi mở form employee
    document.getElementById("txtEmployeeCode").focus();
 }

 /**
  * Lấy element theo tên attribute
  * @param {string} attribute 
  * @returns 
  */
 function getAllElementsWithAttribute(attribute){
    var matchingElements =[];
    var allElements=document.getElementsByTagName('*');

    for(var i = 0 , n =allElements.length;i < n;i++)
    {
        if(allElements[i].getAttribute(attribute) !==null)
        {
            //Elemant exists with attribute.Add to array.
            matchingElements.push(allElements[i])
        }
    }
    return matchingElements ;
 }



 /**
  * Sử lý combobox paging
  * Author:HoanOliver(20/10/2022)
  */

  function btnOpenComboboxPagingOnClick(){
    //Hiển thị form chi tiết nhân viên
    
    document.getElementById("combobox__data-paging").style.display ="block";

   

    
   

  }

   /**
   * Ẩn combobox paging
   * Author:HOanOliver(20/10/2022)
   */

    function btnCloseComboboxPagingFocusOut(){
        //Ẩn combobox  paging
        
      
        document.getElementById("combobox__data-paging").style.display = "none";
       
    
      }



    function updateValueInputComboboxPaging (){
        //Lấy value từ combobox
        let value = this.innerHTML;
        //Gán dữ liệu cho input
        document.getElementById("inputValueComboboxPaging").value =value;
        //Ẩn combobox
        document.getElementById("combobox__data-paging").style.display = "none";
        
    }




    /**
     * Xử lý sidebar thu gọn
     * Author HoanOliver(21/10/2022)
     */
    function handleSideBar(){
        if(!document.getElementById("navbar-container").classList.contains('navbar-container-mini'))
        {

        
        document.getElementById("containerPage").classList.add('container--sidebar-mini');
        document.getElementById("sidebar").classList.add('sidebar-mini');
        document.getElementById("logo-container").style.display="none";
        document.getElementById("navbar-container").classList.add('navbar-container-mini');
        document.getElementById("header-container").classList.add('widthFullParent');
        document.getElementById("text-header").classList.add('ml24');
        

        }   
        else{
            document.getElementById("containerPage").classList.remove('container--sidebar-mini');
            document.getElementById("sidebar").classList.remove('sidebar-mini');
            document.getElementById("logo-container").style.display="flex";
            document.getElementById("navbar-container").classList.remove('navbar-container-mini');
            document.getElementById("header-container").classList.remove('widthFullParent');
            document.getElementById("text-header").classList.remove('ml24');
            

        }
}



    




      

