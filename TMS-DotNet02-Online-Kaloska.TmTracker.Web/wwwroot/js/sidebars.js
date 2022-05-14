let sidebarqweqfdasdf = document.querySelector('.sidebar');
//Event Listeners
sidebarqweqfdasdf.addEventListener('click', Check);

function Check(e) {
    //console.log(e.target);
    const item = e.target;
    if (item.classList[0] === "sidebaractive") {

        var asds = document.querySelector('.active');
        asds.classList.remove('active');
        item.classList.add('active');
    }  
};
