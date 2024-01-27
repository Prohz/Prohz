const url = '../docs/pdf.pdf';

let pdfDoc = null,
    pageNum = 1,
    pageIsRendering = false,
    pageNumIsPending = null;

const scale = 0.8,
    canvas = document.querySelector('#pdf-render'),
    ctx = canvas.getContext('2d');

//Render the page
const renderPage = num => {
    pageIsRendering = true;

    //Get page
    pdfDoc.getPage(num).then(page => {

        //Set scale
        const viewport = page.getViewport({ scale: 1.1 });
        /*const viewport = page.getViewport({ scale: canvas.width / page.getViewport({ scale: 0.5 }).width });*/
        /*const viewport = page.getViewport({ scale: window.screen.width / page.getViewport({ scale: 3 }).width });*/
       
        canvas.height = viewport.height;
        canvas.width = viewport.width;

        const renderCtx = {
            canvasContext: ctx,
            viewport
        }

        page.render(renderCtx).promise.then(() => {
            pageIsRendering = false;

            if (pageNumIsPending !== null) {
                renderPage(pageNumIsPending);
                pageNumIsPending = null;
            }
        });

        //Output current page
        document.querySelector('#page-num').textContent = num;
    });
};

//Check for pages rendering
const queueRenderPage = num => {
    if (pageIsRendering) {
        pageNumIsPending = num;
    } else {
        renderPage(num);
    }
}

// Show prev page
const showPrevPage = () => {
    if (pageNum <= 1) {
        return;
    }
    pageNum--;
    queueRenderPage(pageNum);
}

// Show next page
const showNextPage = () => {
    if (pageNum >= pdfDoc.numPages) {
        return;
    }
    pageNum++;
    queueRenderPage(pageNum);
}

//Get document
pdfjsLib.getDocument(url).promise.then(pdfDoc_ => {
    pdfDoc = pdfDoc_;

    document.querySelector('#page-count').textContent = pdfDoc.numPages;

    renderPage(pageNum)
});

// Button event
document.querySelector('#prev-page').addEventListener('click', showPrevPage);
document.querySelector('#next-page').addEventListener('click', showNextPage);
