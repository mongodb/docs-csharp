Overview
--------

On this page, you can learn how to use the {+driver-long+} to update
fields in |one-or-multiple|. This page describes how to create ``UpdateDefinition<TDocument>``
objects that specify the update operations you want to perform on fields.
You can pass these objects to the update methods described on the |update-page-link|
page.

The {+driver-short+} supports the field update operators described in the
:manual:`{+mdb-server+} manual </reference/operator/update/#fields>`. To specify an
update operation, call the corresponding method from the ``Builders.Update`` property. 
The following sections describe these methods in more detail.

.. tip:: Interactive Lab
   
   This page includes a short interactive lab that demonstrates how to
   modify data by using the ``UpdateManyAsync()`` method. You can complete this
   lab directly in your browser window without installing MongoDB or a code editor.

   To start the lab, click the :guilabel:`Open Interactive Tutorial` button at the
   top of the page. To expand the lab to a full-screen format, click the
   full-screen button (:guilabel:`⛶`) in the top-right corner of the lab pane.

.. include:: /includes/method-overloads.rst

.. include:: /includes/atlas-sample-data.rst

Increment a Value
-----------------

To increment the value of a field by a specific amount, call the ``Builders.Update.Inc()``
method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to increment.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``value``
     - The amount to increment the field by.

       **Data Type:** ``TField``

Multiply a Value
----------------

To multiply the value of a field by a specific amount, call the ``Builders.Update.Mul()``
method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to update.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``value``
     - The amount to multiply the field by.

       **Data Type:** ``TField``

Rename a Field
--------------

To rename a field, call the ``Builders.Update.Rename()`` method. This method accepts
the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to rename.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``newName``
     - The new name for the field.

       **Data Type:** ``string``

Set a Value
-----------

To set the value of a field to a specific value, call the ``Builders.Update.Set()``
method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to update.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``value``
     - The value to set the field to.

       **Data Type:** ``TField``

Set If Lower or Greater
-----------------------

To update the value of the field to a specified value, but only if the specified value
is *greater than* the current value of the field, call the ``Builders.Update.Max()``
method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to update.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``value``
     - The value to set the field to.

       **Data Type:** ``TField``

To update the value of the field to a specified value, but only if the specified value
is *less than* the current value of the field, call the ``Builders.Update.Min()``
method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to update.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``value``
     - The value to set the field to.

       **Data Type:** ``TField``

Set on Insert
-------------

To set the value of a field only if the document was upserted by the same operation, call the
``Builders.Update.SetOnInsert()`` method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to update.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``value``
     - The value to set the field to.

       **Data Type:** ``TField``

Set the Current Date
--------------------

To set the value of a field to the current date and time, call the
``Builders.Update.CurrentDate()`` method. This method accepts the following parameters:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to update.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

   * - ``type``
     - The format of the date and time, defined in the ``UpdateDefinitionCurrentDateType``
       enum. The default value is ``null``.

       **Data Type:** `UpdateDefinitionCurrentDateType? <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.UpdateDefinitionCurrentDateType.html>`__

Unset a Field
-------------

To remove a field from a document, call the ``Builders.Update.Unset()`` method. This
method accepts the following parameter:

.. list-table::
   :widths: 30 70
   :header-rows: 1

   * - Parameter
     - Description

   * - ``field``
     - An expression that specifies the field to remove.

       **Data Type:** ``Expression<Func<TDocument, TField>>``

API Documentation
-----------------

For more information about any of the methods or types discussed in this
guide, see the following API documentation:

- `Builders.Update.Inc() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.Inc-2.html>`__
- `Builders.Update.Mul() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.Mul-2.html>`__
- `Builders.Update.Rename() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.Rename-2.html>`__
- `Builders.Update.Set() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.Set-2.html>`__
- `Builders.Update.Max() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.Max-2.html>`__
- `Builders.Update.Min() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.Min-2.html>`__
- `Builders.Update.SetOnInsert() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.SetOnInsert-2.html>`__
- `Builders.Update.CurrentDate() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.CurrentDate-2.html>`__
- `Builders.Update.Unset() <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.Builders.Update.Unset-2.html>`__