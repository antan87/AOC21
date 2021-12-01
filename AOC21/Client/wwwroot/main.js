async function openFile() {
    const pickerOpts = {
        types: [
            {
                description: 'Text Files',
                accept: {
                    'text/plain': ['.txt'],
                },
            },
        ],
        excludeAcceptAllOption: true,
        multiple: false
    };

    // open file picker
    [fileHandle] = await window.showOpenFilePicker(pickerOpts);

    // get file contents
    const file = await fileHandle.getFile();

    return await file.text();
}