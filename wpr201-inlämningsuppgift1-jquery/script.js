// Samla alla jQuery funktioner så de körs när sidan är laddad
$(document).ready(function() {
  // Kontrollerar att jQuery är korrekt kopplat
  console.log('jQuery is activated');
  
  toggleElementOnClick("#self-description");
  changeStyleOnHover("#self-description");
  performInitialAnimation("#contentbox-middle");
  changeTextOnDoubleClick("#contentbox-middle", "#contentbox-middle .contentbox-text");
  toggleDarkMode();  
});

// 1. Toggla synligheten på ett element som klickas på
function toggleElementOnClick(selector) {
  $(selector).click(function() {
    $(this).toggle();
  });
}

// 2. Ändrar stilen på ett element när man hoverar på det
function changeStyleOnHover(selector) {
  $(selector).hover(function() {
    $(this).toggleClass("new-style");
  });
}

// 3. Animerar ett element att "floata" nedåt till höger och tillbaka vid page load
function performInitialAnimation(selector) {
  
  $(selector).animate({left: '100px', top: '100px'}, 1000)
             .animate({left: '0px', top: '0px'}, 1000);
}

// 4. Ändrar texten på ett element när det eller ett annat specificerat element dubbelklickas på
function changeTextOnDoubleClick(triggerSelector, targetSelector) {
  $(triggerSelector).dblclick(function() {
    $(targetSelector).text("Du dubbelklickade på mig...nu ser jag ut såhär istället!");
  });
}

// 5. Toggla "Dark Mode"
function toggleDarkMode() {
  $('#dark-mode-toggle').click(function() {
    $('body').toggleClass('dark-mode');
  });
}
