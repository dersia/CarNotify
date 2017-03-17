module.exports = function (context, req, registerTable) {
    context.log('JavaScript HTTP trigger function processed a request.');
    context.log(req.params);
    context.log(registerTable);
    if(registerTable !== null) {
        context.bindings.outputQueueItem = 
        {
            notifyurl: registerTable.RegistrationId,
            messageid: req.params.messageid,
            plate: registerTable.RowKey
        } 
    }
    context.log(context.bindings.outputQueueItem);
    res = {
        status: 200
    };
    context.done(null, res);
};