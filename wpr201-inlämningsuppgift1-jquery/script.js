// Samla alla jQuery funktioner så de aktiveras när sidan är laddad
$(document).ready(function() {
  // Kontrollerar att jQuery är korrekt kopplat
  console.log('jQuery is activated');
  
  toggleElementOnClick("#self-description");
  changeStyleOnHover("#self-description");
  performInitialAnimation("#self-description");
  addTooltipOnHover("#self-description");
  toggleDarkMode();  
});

// 1. Toggla synligheten på ett element som klickas på
function toggleElementOnClick(selector) {
  $(selector).click(function() {
    $(this).toggle();
  });
}

// 2. Ändra stilen på ett element när man hoverar på det
function changeStyleOnHover(selector) {
  $(selector).hover(function() {
    $(this).toggleClass("new-style");
  });
}

// 3. Animera in element på skärmen när sidan laddas
function performInitialAnimation(selector) {
  $(selector).animate({left: '0px'}, 2000); // 2000ms = 2s
}

// 4. Addera en tooltip till ett element som hoveras på
function addTooltipOnHover(triggerSelector) {
  $(triggerSelector).hover(function() {
    $(this).attr('title', 'Klicka på mig för att dölja mig!');
  });
}

// 5. Toggla "Dark Mode"
function toggleDarkMode() {
  $('#dark-mode-toggle').click(function() {
    $('body').toggleClass('dark-mode');
  });
}
