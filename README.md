# DataverseUtilities

## Motiviation
Build set of open source tools, plugins, libraries, pcf controls that helps to solve common business problems in PowerApps/Dynamics 365

## Content

### Dataverse plugins

#### Template engine

This plugin helps to generate liquid templates. This could be usefull to generate nice emails or produce html pages rendered on server side

Solution DataverseUtilities_TemplateEngine contains
- Plugin assembly
- Custom Api defintion

Custom API Input Parameters

<table>
	<thead>
		<th>Input parameter name</th>
		<th>Type</th>
		<th>Description</th>
		<th>Example value</th>
	</thead>
	<tbody>
		<tr>
			<td>templateContent</td>
			<td>String</td>
			<td>Liquid template provided as string</td>
			<td>
				<pre>
&lt;p&gt;{{ user.name }} has to do:&lt;/p&gt;
&lt;ul>
	{% for item in user.tasks -%}
	&lt;li&gt;{{ item.name }}&lt;/li&gt;
	{% endfor -%}
&lt;/ul&gt;
				</pre>
			</td>
		</tr>
		<tr>
			<td>templateModelStr</td>
			<td>String</td>
			<td>Json object that suppose to be used in liquid</td>
			<td>
				<pre>
{
	"user"{
	"name":"Krzysztof",
	"tasks":[
		{"name":"Hello World!"},
		{"name":"Hello World2!"}
		]
	}
}
				</pre>
			</td>
		</tr>
	</tbody>
</table>




Custom API Output Parameters


<table>
	<thead>
		<th>Output parameter name</th>
		<th>Type</th>
		<th>Description</th>
	</thead>
	<tbody>
		<tr>
			<td>renderedTemplate</td>
			<td>string</td>
			<td>Rendered liquid template</td>
		</tr>
	</tbody>
</table>


 :warning: **Recommendations**: It is recommended to use this plugin to generate light liquid templates to produce emails or simple html documents.


 Template Engine uses Dot Liquid library (https://www.nuget.org/packages/dotliquid) 

#### Pdf Engine


Solution DataverseUtilities_PdfEngine contains
- Plugin assembly
- Custom Api defintion


Custom API Input Parameters

<table>
	<thead>
		<th>Input parameter name</th>
		<th>Type</th>
		<th>Description</th>
	</thead>
	<tbody>
		<tr>
			<td>htmlContent</td>
			<td>String</td>
			<td>Html document</td>
		</tr>
	</tbody>
</table>

Custom API Output Parameters


<table>
	<thead>
		<th>Output parameter name</th>
		<th>Type</th>
		<th>Description</th>
	</thead>
	<tbody>
		<tr>
			<td>pdfContent</td>
			<td>string / base64 encoded data</td>
			<td>Generated pdf</td>
		</tr>
	</tbody>
</table>

 :warning: **Recommendations**: It is recommended to use this plugin to generate light pdf documents.


Pdf Engine uses iText 7 library to generate pdf 
- https://www.nuget.org/packages/itext7 
-  https://www.nuget.org/packages/itext7.pdfhtml