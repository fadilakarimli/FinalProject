//"use strict";

//let btnss = document.querySelectorAll(".delete-brand");
//btnss.forEach((btn) => {
//    btn.addEventListener("click", function () {
//        let brandId = parseInt(this.getAttribute("data-id"));
//        deleteBrand(brandId)
//        this.closest("tr").remove();
//    })

//});


//async function deleteBrand(brandId) {
//    try {
//        const response = await fetch(`/admin/brand/delete?id=${brandId}`, {
//            method: `POST`,
//            headers: {
//                'Content-Type': `application/json`
//            },
//        });
//        if (response.ok) {
//            console.log("Brand deleted");
//        } else {
//            console.eror("failed to deleted . Status", response.status);
//        }

//    } catch (eror) {
//        console.error("Fetch error:", error);
//    }
//}
