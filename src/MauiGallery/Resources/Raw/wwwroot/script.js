// JS ⇄ C# bridge provided by HybridWebView (see HybridWebView.js in dotnet/maui).
function log(text) {
    document.getElementById('log').innerText = text;
}

function sendToCsharp() {
    // JS → C#: raw string message, received via HybridWebView.RawMessageReceived
    window.HybridWebView.SendRawMessage('hello from JavaScript @ ' + new Date().toLocaleTimeString());
    log('已发送原始消息 / Raw message sent');
}

async function invokeCsharp() {
    // JS → .NET: invokes a public method on the target set by SetInvokeJavaScriptTarget(...)
    const result = await window.HybridWebView.InvokeDotNet('Greet', { name: 'MAUI' });
    log('InvokeDotNet(Greet) => ' + result);
}

async function askCsharpValue() {
    const result = await window.HybridWebView.InvokeDotNet('Add', [2, 40]);
    log('InvokeDotNet(Add 2, 40) => ' + result);
}

// C# → JS: invoked from C# via EvaluateJavaScriptAsync / InvokeJavaScriptAsync
function greetFromCsharp(name) {
    log('C# 调用了 JS：Hello ' + name);
    return 'Hello ' + name + ' from JavaScript';
}
