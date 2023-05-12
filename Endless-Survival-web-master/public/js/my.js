function InputTextChange(){
    document.getElementById("input-text").className = "d-none";
    document.getElementById("avatar").className = "profile-edit-btn";
}

var timeout;
function showTooltip(match) {
    clearTimeout(timeout);

    var tooltip = document.getElementById("tooltip");
    tooltip.style.display = "none";
    var tooltip = document.getElementById("tooltip_reg");
    tooltip.style.display = "none";

    var circle = document.getElementById("circle");
    var line = document.getElementById("line");

    if (match != null) {
        var primary_weapon = document.getElementById("primary_weapon");
        var secondary_weapon = document.getElementById("secondary_weapon");
        var kills = document.getElementById("kills");
        var death = document.getElementById("death");
        var damage_taken = document.getElementById("damage_taken");
        var time = document.getElementById("time");
        var date = document.getElementById("date");
        var play_time = document.getElementById("play_time");

        primary_weapon.innerHTML = match.primary_weapon;
        secondary_weapon.innerHTML = match.secondary_weapon;
        kills.innerHTML = match.kills;
        death.innerHTML = match.death;
        damage_taken.innerHTML = match.damage_taken;
        time.innerHTML = match.time;
        date.innerHTML = match.date;
        play_time.innerHTML = match.play_time;

        var point = document.getElementById(match.id);
        var rect = point.getBoundingClientRect();
        
        var tooltip = document.getElementById("tooltip");

        circle.setAttribute("cx", match.pos_x);
        circle.setAttribute("cy", match.pos_y);
    
        line.setAttribute("x1", match.pos_x);
        line.setAttribute("x2", match.pos_x);

        tooltip.style.left = rect.left - 116.5 + 'px';
        var top = rect.top - 210;
        tooltip.style.top = (top > 0 ? top : (top + 220)) + 'px';
    }
    else {
        var point = document.getElementById(0);
        var rect = point.getBoundingClientRect();
        var tooltip = document.getElementById("tooltip_reg");

        circle.setAttribute("cx", 0);
        circle.setAttribute("cy", "86%");
    
        line.setAttribute("x1", 0);
        line.setAttribute("x2", 0);

        tooltip.style.left = rect.left - 116.5 + 'px';
        tooltip.style.top = rect.top - 55 + 'px';
    }

    tooltip.style.display = "inline";
}
  
function hideTooltip() {
    clearTimeout(timeout);
    timeout = setTimeout( function()
        {
            var tooltip = document.getElementById("tooltip");
            tooltip.style.display = "none";
            var tooltip = document.getElementById("tooltip_reg");
            tooltip.style.display = "none";

            var point = document.getElementById("circle");
            circle.setAttribute("cx", -10);
            circle.setAttribute("cy", -10);

            let line = document.getElementById("line");
            line.setAttribute("x1", -10);
            line.setAttribute("x2", -10);
        }, 
        2000
    )
}